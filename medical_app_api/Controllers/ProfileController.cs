using medical_app_db.Core.Models;
using medical_app_db.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace medical_app_db.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

                return Ok("Profile updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
