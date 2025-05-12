using AutoMapper;
using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models.Doctor_Module;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace medical_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentService appointmentService,IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(DateTime? appointmentDate)
        {
            try
            {

            var appointments = await _appointmentService.GetAppointmentsAsync(appointmentDate);
            return Ok(new 
            { 
                message = "Suceess", 
                data = _mapper.Map<List<AppointmentDTO>>(appointments), 
                StatusCode = HttpStatusCode.OK 
            });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentAsync(id);
            if (appointment is null) 
                return NotFound(new 
                { 
                    message = "Not Found", 
                    data = appointment, 
                    StatusCode = HttpStatusCode.NotFound 
                });
            
            return Ok(new 
            { 
                message = "Suceess", 
                data = _mapper.Map<AppointmentDTO>(appointment), 
                statusCode = HttpStatusCode.OK 
            });
        }

        [HttpPut("accept/{id}")]
        public async Task<IActionResult> Accept([FromRoute] Guid id)
        {
            var result = await _appointmentService.AcceptApointment(id);
            if(!result) 
                return BadRequest(
                    new { 
                        data = result,
                        message = "Bad Request",
                        StatusCode = HttpStatusCode.BadRequest
                    });

            return Ok(
                new
                {
                    data = result,
                    message = "Appointment Accepted",
                    StatusCode = HttpStatusCode.OK
                });
        }

        [HttpPut("decline/{id}")]
        public async Task<IActionResult> Decline([FromRoute] Guid id)
        {
            var result = await _appointmentService.DeclineApointment(id);
            if (!result)
                return BadRequest(
                    new
                    {
                        data = result,
                        message = "Bad Request",
                        StatusCode = HttpStatusCode.BadRequest
                    });

            return Ok(
                new
                {
                    data = result,
                    message = "Appointment Declined",
                    StatusCode = HttpStatusCode.OK
                });
        }

        [HttpPost("add-prescription")]
        public async Task<IActionResult> AddPrescription(PrescriptionDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new 
                {message = "Bad Request",
                    data = model , 
                    StatusCode = HttpStatusCode.BadRequest
                });
            try
            {

                var newPrescription = await _appointmentService.AddPrescriptionAsync(model);


                if(newPrescription is null)
                    return BadRequest(new
                    {
                        message = "Coudn't Add Prescription",
                        data = model,
                        StatusCode = HttpStatusCode.BadRequest
                    });


            return Ok(new
            {
                message = "Suceess",
                data = _mapper.Map<PrescriptionDTO>(newPrescription),
                statusCode = HttpStatusCode.OK
            });
            }catch(Exception ex)
            {
                return BadRequest(new{ m = ex.StackTrace , x = ex.Message});
            }
        }

		[HttpGet("get-all-appointment-dates")]
		public async Task<IActionResult> GetAllAppointmentDates(DateTime? appointmentDate)
		{
			var appointments = await _appointmentService.GetAppointmentDates();
			return Ok(new
			{
				message = "Suceess",
				data = appointments,
				StatusCode = HttpStatusCode.OK
			});
		}

		[HttpPut("edit-dates/{appointment_date_id}")]
        public async Task<IActionResult> editClincAppointmentDates(Guid appointment_date_id, AppointmentDateDTO appointmentDate)
        {
			if (appointmentDate == null)
			{
				return BadRequest(new { message = "Invalid appointment date data", statusCode = (int)HttpStatusCode.BadRequest });
			}

			try
			{
				var updatedAppointmentDate = await _appointmentService.UpdateAppointmentDateAsync(appointment_date_id, appointmentDate);

				if (updatedAppointmentDate == null)
				{
					return NotFound(new { message = "Appointment date not found", statusCode = (int)HttpStatusCode.NotFound });
				}

				return Ok(new
				{
					message = "Appointment date updated successfully",
					statusCode = (int)HttpStatusCode.OK,
					data = updatedAppointmentDate
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Failed to update appointment date", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
			}
		}
    }
}
