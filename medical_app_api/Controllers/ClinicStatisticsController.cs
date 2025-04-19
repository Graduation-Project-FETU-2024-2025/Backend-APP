using Microsoft.AspNetCore.Mvc;
using medical_app_db.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net;
using System;
using System.Threading.Tasks;

namespace medical_app_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicStatisticsController : ControllerBase
    {
        private readonly IClinicStatisticsService _clinicStatisticsService;

        public ClinicStatisticsController(IClinicStatisticsService clinicStatisticsService)
        {
            _clinicStatisticsService = clinicStatisticsService;
        }

        private Guid GetClinicIdFromToken()
        {
            var clinicIdClaim = User.FindFirst("ClinicId")?.Value;
            if (string.IsNullOrEmpty(clinicIdClaim) || !Guid.TryParse(clinicIdClaim, out Guid clinicId))
            {
                throw new UnauthorizedAccessException("Clinic ID is missing or invalid in the token.");
            }
            return clinicId;
        }

        [HttpGet]
        public async Task<IActionResult> GetClinicStatistics()
        {
            try
            {
                var clinicId = GetClinicIdFromToken();
                var stats = await _clinicStatisticsService.GetClinicStatisticsAsync(clinicId);

                if (stats == null)
                {
                    return NotFound(new { message = "Statistics not found", statusCode = (int)HttpStatusCode.NotFound });
                }

                var responseData = new
                {
                    message = "Success",
                    statusCode = (int)HttpStatusCode.OK,
                    data = new
                    {
                        ReVisitCount = stats.ReVisitCount,
                        NewVisitCount = stats.NewVisitCount,
                        CheckupCount = stats.CheckupCount,
                        PendingAppointments = stats.PendingAppointments,
                        ConfirmedAppointments = stats.ConfirmedAppointments,
                        CanceledAppointments = stats.CanceledAppointments,
                        TotalIncome = stats.TotalIncome
                    }
                };

                return Ok(responseData);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message, statusCode = (int)HttpStatusCode.Unauthorized });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
            }
        }
    }
}