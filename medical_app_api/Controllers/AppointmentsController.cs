using AutoMapper;
using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models.Doctor_Module;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            var appointments = await _appointmentService.GetAppointmentsAsync(appointmentDate);
            return Ok(new 
            { 
                message = "Suceess", 
                data = _mapper.Map<List<AppointmentDTO>>(appointments), 
                StatusCode = HttpStatusCode.OK 
            });
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

        [HttpPost("add-prescription")]
        public async Task<IActionResult> AddPrescription(PrescriptionDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new 
                {message = "Bad Request",
                    data = model , 
                    StatusCode = HttpStatusCode.BadRequest
                });

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
        }
    }
}
