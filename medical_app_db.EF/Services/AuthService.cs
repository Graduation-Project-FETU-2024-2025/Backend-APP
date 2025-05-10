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
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.EF.Factory;
using Microsoft.EntityFrameworkCore;
namespace medical_app_db.EF.Services
{
    public class AuthService : IAuthService
    {
        private readonly MedicalDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private IMemoryCache _cach;
        private readonly JWT _Jwt;
        private ClaimsStrategyFactory _claimsStrategyFactory;
        private readonly IUserFactory _userFactory;

        public AuthService(
            MedicalDbContext context,
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            IOptions<JWT> jwtOptions,
            IMemoryCache cache,
            IUserFactory userFactory)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
            _Jwt = jwtOptions.Value;
            _cach = cache;
            _userFactory = userFactory;
            _claimsStrategyFactory = new ClaimsStrategyFactory(context);
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
                    Message = "UserName already exists",
                    Status = HttpStatusCode.BadRequest
                };

            try
            {
                var user = _userFactory.CreateUser(model);

                var result = await _userManager.CreateAsync(user, model.Password!);

                if (user is Doctor doctor)
                {
                    doctor.DoctorClinic.DoctorId = doctor.Id;
                    await _context.SaveChangesAsync();
                }

                if (!result.Succeeded)
                    throw new Exception();

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }
            }
            catch(Exception ex)
            {
                return new AuthModel
                {
                    Message = ex.Message,
                    Status = HttpStatusCode.InternalServerError
                };
            }

            return new AuthModel
            {
                Message = "Account Created Successfully",
                IsAuthuntecated = true,
                Status = HttpStatusCode.Created
            };
        }
        public async Task<AuthModel> Login(LoginDTO model)
        {
            if (model.Email.IsNullOrEmpty())
                return new AuthModel
                {
                    Message = "Email is required",
                    Status = HttpStatusCode.BadRequest
                };

            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user is null)
                return new AuthModel
                {
                    Message = "User not found",
                    Status = HttpStatusCode.NotFound
                };

            var otp = GenerateOTP();

            try
            {
                await _emailService.SendEmailAsync(user.Email, "Login Confimatation", otp);
                _cach.Set(user.Email!, otp, TimeSpan.FromMinutes(300));
            }
            catch
            {
                return new AuthModel
                {
                    Message = "Coudn't Send Email, Plaese Try again",
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

            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user is null)
                return new AuthModel
                {
                    Message = "Email Not Found",
                    Status = HttpStatusCode.BadRequest
                };

            _cach.TryGetValue(model.Email!, out string? chachedOtp);

            if (chachedOtp is null)
                return new AuthModel
                {
                    Message = "OTP Expired, Try Agian",
                    Status = HttpStatusCode.BadRequest
                };

            if (chachedOtp != model.OTP)
                return new AuthModel
                {
                    Message = "OTP Is Not Correct, Please Check your Email and try again",
                    Status = HttpStatusCode.BadRequest
                };

            var token = await GenerateJwtToken(user);

            var result = new AuthModel
            {
                Message = "Login Successful",
                IsAuthuntecated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo,
                Email = user.Email,
                UserName = user.UserName,
                Roles = _userManager.GetRolesAsync(user).Result,
                Status = HttpStatusCode.OK
            };

            return result;
        }
        private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new(ClaimTypes.Name, user.UserName ?? "")
            };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claimsStrategy = _claimsStrategyFactory.GetStrategy(user);
            if (claimsStrategy != null)
            {
                claims.AddRange(claimsStrategy.GetClaims(user));
            }


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
