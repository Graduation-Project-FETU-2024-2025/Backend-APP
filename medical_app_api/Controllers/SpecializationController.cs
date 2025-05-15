using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace medical_app_api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SpecializationController : ControllerBase
	{
		private readonly ISpecializationService _specializationService;

		public SpecializationController(ISpecializationService specializationService)
		{
			_specializationService = specializationService;
		}

		[HttpGet]
		public async Task<IActionResult> getAllSpecialization(int page = 1, int pageSize = 3, String search = "")
		{
			var lang = Request.Headers["lang"].ToString().ToLower();

			if (string.IsNullOrEmpty(lang))
			{
				return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
			}

			var specializations = await _specializationService.GetAllSpecializationsAsync(page, pageSize, search);

			if (specializations == null || !specializations.Any())
			{
				return NoContent();
			}

			var result = specializations.Select(s => new
			{
				Id = s.Id,
				Name = lang == "ar" ? s.ArName : s.EnName,
				ArName = s.ArName,
				EnName = s.EnName,
				Icon = s.Icon,
				
			});

			return Ok(new { message = "Success", statusCode = (int)HttpStatusCode.OK, data = result });
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetSpecializationById(Guid id)
		{
			// Validate the branch is accessibly to the account
			var lang = Request.Headers["lang"].ToString().ToLower();

			if (string.IsNullOrEmpty(lang))
			{
				return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
			}

			try
			{
				var specialization = await _specializationService.GetSpecializationAsync(id);

				if (specialization == null)
				{
					return NotFound(new { message = "Specialization not found", statusCode = (int)HttpStatusCode.NotFound });
				}

				var result = new
				{
					Id = specialization.Id,
					Name = lang == "ar" ? specialization.ArName : specialization.EnName,
					ArName = specialization.ArName,
					EnName = specialization.EnName,
					Icon = specialization.Icon,
				};

				return Ok(new { message = "Success", statusCode = (int)HttpStatusCode.OK, data = result });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Internal server error", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddSpecialization([FromForm] SpecializationDto specializationDto, IFormFile icon)
		{
			if (specializationDto == null)
			{
				return BadRequest(new { message = "Invalid specialization data", statusCode = (int)HttpStatusCode.BadRequest });
			}

			try
			{
				var createSpecialization = await _specializationService.AddSpecializationAsync(specializationDto, icon);

				return Ok(
					new
					{
						message = "Specialization has been added successfully",
						statusCode = (int)HttpStatusCode.Created,
						data = specializationDto
					});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = ex.Message, statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBranchProduct(Guid id, [FromForm] SpecializationDto specializationDto, IFormFile? icon)
		{
			if (specializationDto == null)
			{
				return BadRequest(new { message = "Invalid specialization data", statusCode = (int)HttpStatusCode.BadRequest });
			}

			try
			{
				var updatedSpecialization = await _specializationService.UpdateSpecializationAsync(id, specializationDto, icon);

				if (updatedSpecialization == null)
				{
					return NotFound(new { message = "Specialization not found", statusCode = (int)HttpStatusCode.NotFound });
				}

				return Ok(new
				{
					message = "Specizalization updated successfully",
					statusCode = (int)HttpStatusCode.OK,
					data = updatedSpecialization
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Failed to update specialization", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSpecialization(Guid id)
		{
			try
			{
				var result = await _specializationService.DeleteSpecializationAsync(id);

				if (!result)
				{
					return NotFound(new { message = "Specialization not found", statusCode = (int)HttpStatusCode.NotFound });
				}

				return Ok(new { message = "Specialization deleted successfully", statusCode = (int)HttpStatusCode.OK });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Failed to delete specizalization", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
			}
		}
	}
}
