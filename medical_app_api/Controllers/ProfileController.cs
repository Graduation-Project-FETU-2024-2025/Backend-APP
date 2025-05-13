using medical_app_db.Core.Models;
using medical_app_db.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace medical_app_db.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var profile = await _profileService.GetProfileAsync();
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] ProfileDto dto, IFormFile? image)
        {
            try
            {
                var result = await _profileService.EditProfileAsync(dto,image);
                if (!result)
                    return BadRequest("Update failed");

                return Ok(new {Message =  "Profile updated successfully" , status = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
