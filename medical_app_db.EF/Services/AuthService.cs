using medical_app_db.Core.Interfaces;
using medical_app_db.Core.DTOs;
using medical_app_db.Core.Models;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using medical_app_db.Core.Helpers;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
namespace medical_app_db.EF.Services
{
    public class AuthService : IAuthService
    {
        private readonly MedicalDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly IEmailService _emailService;
        private IMemoryCache _cach;
        private readonly JWT _Jwt;

        public AuthService(
            MedicalDbContext context,
            UserManager<Account> userManager,
            IEmailService emailService,
            IOptions<JWT> jwtOptions, 
            IMemoryCache cache)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
            _Jwt = jwtOptions.Value;
            _cach = cache;
        }

        public async Task<AuthModel> Register(RegisterationDTO model)
        {
            if (await _userManager.FindByEmailAsync(model.Email!) != null)
                return new AuthModel 
                { 
                    Message = "Email already exists", 
                    Status = HttpStatusCode.BadRequest 
                };

            if (await _userManager.FindByNameAsync(model.UserName!) != null)
                return new AuthModel 
                { 
                    Message = "UserName already exists" , 
                    Status = HttpStatusCode.BadRequest
                };

            var account = new Account
            {
                Id =  Guid.NewGuid() ,
                UserName = model.UserName,
                Email = model.Email,
                Image = model.Image,
                PharmacyId = model.PharmacyId
            };

            var result = await _userManager.CreateAsync(account, model.Password!);

            if(!result.Succeeded)
                return new AuthModel 
                { 
                    Message = "Failed to Create Account", 
                    Status = HttpStatusCode.BadRequest 
                };

            return new AuthModel 
            { 
                Message = "Account Created Successfully" , 
                IsAuthuntecated = true, 
                Status = HttpStatusCode.Created 
            };
        }
        public async Task<AuthModel> Login(LoginDTO model)
        {
            if(model.Email.IsNullOrEmpty())
                return new AuthModel 
                { 
                    Message = "Email is required" , 
                    Status = HttpStatusCode.BadRequest 
                }; 

            var account = await _userManager.FindByEmailAsync(model.Email!);

            if(account is null)
                return new AuthModel 
                { 
                    Message = "User not found" ,
                    Status = HttpStatusCode.NotFound
                };

            var otp = GenerateOTP();

            try
            {
                await _emailService.SendEmailAsync(account.Email, "Login Confimatation", otp);
                _cach.Set(account.Email!, otp, TimeSpan.FromMinutes(5));
            }
            catch
            {
                return new AuthModel 
                { 
                    Message = "Coudn't Send Email, Plaese Try again" , 
                    Status = HttpStatusCode.BadRequest 
                };
            }

            return new AuthModel 
            { 
                Message = "OTP sent to your email", 
                Status = HttpStatusCode.OK 
            };
        }
        public async Task<AuthModel> VerifytOtp(TestOtpDTO model)
        {
            if (model.Email.IsNullOrEmpty())
                return new AuthModel { Message = "Email is Required" };
            if (model.OTP.IsNullOrEmpty())
                return new AuthModel { Message = "OTP is required" };

            var account = await _userManager.FindByEmailAsync(model.Email!);

            if (account is null)
                return new AuthModel 
                { 
                    Message = "Email Not Found" , 
                    Status = HttpStatusCode.BadRequest 
                };

            _cach.TryGetValue(model.Email!, out string? chachedOtp);

            if (chachedOtp is null)
                return new AuthModel 
                { 
                    Message = "OTP Expired, Try Agian" , 
                    Status = HttpStatusCode.BadRequest
                };

            if (chachedOtp != model.OTP)
                return new AuthModel 
                { 
                    Message = "OTP Is Not Correct, Please Check your Email and try again" , 
                    Status = HttpStatusCode.BadRequest 
                };

            var token = await GenerateJwtToken(account);
            var result = new AuthModel
            {
                Message = "Login Successful",
                IsAuthuntecated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo,
                Email = account.Email,
                UserName = account.UserName,
                Roles = _userManager.GetRolesAsync(account).Result,
                Status = HttpStatusCode.OK
            };

            return result;
        }
        private async Task<JwtSecurityToken> GenerateJwtToken(Account account)
        {
            var accountClaims = await _userManager.GetClaimsAsync(account);
            var roles = await _userManager.GetRolesAsync(account);

            var roleClaims = new List<Claim>();
            foreach (var claim in roles)
            {
                roleClaims.Add(new Claim("role", claim));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email , account.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Iss , "healthApp"),
                new Claim(ClaimTypes.Name, account.UserName ?? ""),
                new Claim("PharmacyID", account.PharmacyId.ToString() ?? ""),
            }.Union(accountClaims).Union(roleClaims);

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.SecurityKey ?? "healthApp"));
            var signInCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: _Jwt.IssureIP,
                audience: _Jwt.AudienceIP,
                claims: claims,
                expires: DateTime.Now.AddDays(_Jwt.DurationInDays),
                signingCredentials: signInCredentials
            );

            return securityToken;
        }
        private static string GenerateOTP()
        {
            Random randomGenerator = new Random();
            var randomNumber = randomGenerator.Next(0, 10000);

            return randomNumber.ToString("0000");
        }
        
    }
}
