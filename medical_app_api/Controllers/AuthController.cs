using medical_app_db.Core.DTOs;
using medical_app_db.Core.Helpers;
using medical_app_db.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace medical_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //[Authorize("Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterationDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(model);

            if (!result.IsAuthuntecated)
                return BadRequest(new BaseResponse()
                    {
                        Message = result.Message,
                        StatusCode = result.Status
                    }
                );

            return Ok(result);
        }

        [HttpPost("get-otp")]
        public async Task<IActionResult> GetOtp(LoginDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(new BaseResponse()
                {
                    Message = "Email is Required",
                    StatusCode = HttpStatusCode.BadRequest
                });

            var result = await _authService.GetOtpAsync(model);

            if(result.Status != HttpStatusCode.OK ) 
                return BadRequest(new BaseResponse()
                {
                    Message = result.Message,
                    StatusCode = result.Status
                });

            return Ok(new BaseResponse()
            {
                Message = result.Message,
                StatusCode = result.Status
            });
        }
        
        [HttpPost("user/login")]
        public async Task<IActionResult> UserLogin(UserLoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.UserLogin(model);

            if (!result.IsAuthuntecated)
                return BadRequest(new
                {
                    result.Message,
                    StatusCode = result.Status
                });


            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(TestOtpDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(new BaseResponse()
                {
                    Message = "Email and OTP are required",
                    StatusCode = HttpStatusCode.BadRequest
                });

            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthuntecated) 
                return BadRequest(new BaseResponse()
                {
                    Message = result.Message,
                    StatusCode = result.Status
                });

            return Ok(result);
        }
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(TestOtpDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var token = await _authService.GetPasswordResetToken(model);
            if (string.IsNullOrEmpty(token))
                return BadRequest(new BaseResponse()
                {
                    Message = "OTP Expired",
                    StatusCode = HttpStatusCode.BadRequest
                });
            return Ok(new
            {
                Message = "Reset Token Generated",
                ResetToken = token,
                StatusCode = HttpStatusCode.OK
            });

        }
        
        [HttpPost("change-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.ResetPasswordAsync(model);
            if (!result)
                return BadRequest(new
                {
                    Message = "Some Error Happened, Try again",
                    StatusCode = HttpStatusCode.BadRequest
                });
            return Ok(new
            {
                Message = "Password Updated Sucessfully",
                StatusCode = HttpStatusCode.OK
            });
        }
    }
}
