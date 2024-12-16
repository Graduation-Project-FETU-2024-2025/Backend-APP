using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
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
        public async Task<IActionResult> Register(RegisterationDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(model);

            if(!result.IsAuthuntecated)
                return BadRequest(new 
                {
                    result.Message, 
                    result.Status
                });

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest("Email is required");

            var result = await _authService.Login(model);

            if(result.Status != HttpStatusCode.OK ) 
                return BadRequest(new
                {
                    result.Message,
                    result.Status
                });

            return Ok(new
            {
                result.Message,
                result.Status
            });
        }

        [HttpPost("test-otp")]
        public async Task<IActionResult> TestOtp(TestOtpDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest("Email and OTP are required");

            var result = await _authService.VerifytOtp(model);

            if (!result.IsAuthuntecated) 
                return BadRequest(new
                {
                    result.Message,
                    result.Status
                });

            return Ok(result);
        }
    }
}
