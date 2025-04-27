using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace medical_app_api.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ClinicController : Controller
	{
		private readonly IClinicService _clinicService;

		public ClinicController(IClinicService clinicService)
		{
			_clinicService = clinicService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetClinicById(Guid id)
		{
			var clinic = await _clinicService.GetClinicByIdAsync(id);

			return Ok(new
			{
				message = "Success",
				statusCode = (int)HttpStatusCode.OK,
				data = clinic
			});
		}

		[HttpPut]
		public async Task<IActionResult> UpdateBranchProduct(ClinicDTO clinicDTO)
		{
			if (clinicDTO == null)
			{
				return BadRequest(new { message = "Invalid clinic data", statusCode = (int)HttpStatusCode.BadRequest });
			}

			try
			{
				var updatedClinic = await _clinicService.UpdateClinicAsync(clinicDTO);

				if (updatedClinic == null)
				{
					return NotFound(new { message = "Clinic not found", statusCode = (int)HttpStatusCode.NotFound });
				}

				return Ok(new
				{
					message = "Clinic updated successfully",
					statusCode = (int)HttpStatusCode.OK,
					data = updatedClinic
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Failed to update clinic", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
			}
		}
	}
}
