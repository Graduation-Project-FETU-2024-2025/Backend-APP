using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[Authorize(Roles = "User")]
[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _doctorService.GetAllDoctorsAsync(page, pageSize);
        return Ok(new
        {
            message = "Doctors retrieved successfully",
            statusCode = (int)HttpStatusCode.OK,
            data = result
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        if (doctor == null)
        {
            return NotFound(new
            {
                message = "Doctor not found",
                statusCode = (int)HttpStatusCode.NotFound
            });
        }

        return Ok(new
        {
            message = "Doctor retrieved successfully",
            statusCode = (int)HttpStatusCode.OK,
            data = doctor
        });
    }

    [HttpGet("specialization/{specializationId}")]
    public async Task<IActionResult> GetBySpecialization(Guid specializationId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _doctorService.GetDoctorsBySpecializationAsync(specializationId, page, pageSize);
        return Ok(new
        {
            message = "Doctors by specialization retrieved successfully",
            statusCode = (int)HttpStatusCode.OK,
            data = result
        });
    }

    [HttpGet("top-rated")]
    public async Task<IActionResult> GetTopRated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _doctorService.GetTopRatedDoctorsAsync(page, pageSize);
        return Ok(new
        {
            message = "Top rated doctors retrieved successfully",
            statusCode = (int)HttpStatusCode.OK,
            data = result
        });
    }

    [HttpGet("top-rated/specialization/{specializationId}")]
    public async Task<IActionResult> GetTopRatedBySpecialization(Guid specializationId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _doctorService.GetTopRatedDoctorsBySpecializationAsync(specializationId, page, pageSize);
        return Ok(new
        {
            message = "Top rated doctors by specialization retrieved successfully",
            statusCode = (int)HttpStatusCode.OK,
            data = result
        });
    }
}
