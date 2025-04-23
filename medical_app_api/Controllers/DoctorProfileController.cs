using System;
using System.Threading.Tasks;
using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace medical_app_api.Controllers
{
    [Route("api/secure/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorProfileController : ControllerBase
    {
        private readonly IDoctorProfileServices _doctorProfileServices;

        public DoctorProfileController(IDoctorProfileServices doctorProfileServices)
        {
            _doctorProfileServices = doctorProfileServices;
        }

        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var lang = Request.Headers["lang"].ToString().ToLower();

            if (string.IsNullOrEmpty(lang))
            {
                return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
            }

            try
            {
                var doctorProfile = await _doctorProfileServices.GetDoctorProfileAsync();

                return Ok(new
                {
                    message = "Success",
                    statusCode = (int)HttpStatusCode.OK,
                    data = new
                    {
                        doctorProfile.Id,
                        doctorProfile.ApplicationUserId,
                        Name = lang == "ar" ? doctorProfile.AR_Name : doctorProfile.EN_Name,
                        doctorProfile.DateOfBirth,
                        doctorProfile.Picture,
                        doctorProfile.Specialization,
                        doctorProfile.Gender,
                        doctorProfile.SSN
                    }
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, statusCode = (int)HttpStatusCode.NotFound });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message, statusCode = (int)HttpStatusCode.Unauthorized });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the doctor profile.", statusCode = (int)HttpStatusCode.InternalServerError, error = ex.Message });
            }
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] DoctorDTO doctorDto)
        {
            if (doctorDto == null || doctorDto.Id == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid doctor data", statusCode = (int)HttpStatusCode.BadRequest });
            }

            try
            {
                var updatedDoctor = await _doctorProfileServices.UpdateDoctorProfileAsync(doctorDto);

                return Ok(new
                {
                    message = "Profile updated successfully",
                    statusCode = (int)HttpStatusCode.OK,
                    data = new
                    {
                        updatedDoctor.Id,
                        updatedDoctor.ApplicationUserId,
                        updatedDoctor.EN_Name,
                        updatedDoctor.AR_Name,
                        updatedDoctor.Picture
                    }
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, statusCode = (int)HttpStatusCode.NotFound });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message, statusCode = (int)HttpStatusCode.Unauthorized });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the doctor profile.", statusCode = (int)HttpStatusCode.InternalServerError, error = ex.Message });
            }
        }
    }
}
