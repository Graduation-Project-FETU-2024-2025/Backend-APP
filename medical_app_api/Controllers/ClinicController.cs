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
	}
}
