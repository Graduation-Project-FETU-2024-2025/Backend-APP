using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;
namespace medical_app_db.API.Controllers;

[Authorize(Roles = "Account")]
[Route("api/secure/[controller]")]
[ApiController]
public class BranchController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllBranches(int page = 1, int pageSize = 3)
    {
        try
        {
      
            var lang = Request.Headers["lang"].ToString().ToLower();

            if (string.IsNullOrEmpty(lang))
            {
                return BadRequest(new
                {
                    message = "Language not provided in the header.",
                    statusCode = (int)HttpStatusCode.BadRequest
                });
            }

         
            var branches = await _branchService.GetAllBranchesAsync( lang,page, pageSize);

            if (branches == null || !branches.Any())
            {
                return NoContent();
            }

            
            return Ok(new
            {
                message = "Success",
                statusCode = (int)HttpStatusCode.OK,
                data = branches
            });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new
            {
                message = "An error occurred while fetching branches.",
                error = ex.Message
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranchById(Guid id)
    {
        var lang = Request.Headers["lang"].ToString().ToLower();

        if (string.IsNullOrEmpty(lang))
        {
            return BadRequest(new
            {
                message = "Language not provided in the header.",
                statusCode = (int)HttpStatusCode.BadRequest
            });
        }

        var branch = await _branchService.GetBranchByIdAsync(id, lang);

        return Ok(new
        {
            message = "Success",
            statusCode = (int)HttpStatusCode.OK,
            data = branch
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddBranch([FromForm]BranchDTO branchDto,IFormFile? image,[FromForm] string? workingHours)
    {
        if (branchDto == null)
        {
            return BadRequest(new { message = "Invalid branch data", statusCode = (int)HttpStatusCode.BadRequest });
        }
        if (image is null)
            return BadRequest(new { message = "Image is Required", statusCode = (int)HttpStatusCode.BadRequest });
        try
        {
            if (!string.IsNullOrEmpty(workingHours))
            {
                branchDto.WorkingHours = JsonSerializer.Deserialize<List<WorkingPeriodDTO>>(workingHours);
            }

            var createdBranch = await _branchService.AddBranchAsync(branchDto ,image);

            return CreatedAtAction(nameof(GetBranchById), new { id = createdBranch.Id }, new
            {
                message = "Branch created successfully",
                statusCode = (int)HttpStatusCode.Created,
                data = createdBranch
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to create branch", statusCode = (int) HttpStatusCode.InternalServerError, details = ex.Message });
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBranch([FromRoute]Guid id, [FromForm]BranchDTO branchDto,IFormFile? image, [FromForm] string? workingHours)
    {
        if (branchDto == null)
        {
            return BadRequest(new { message = "Invalid branch data", statusCode = (int)HttpStatusCode.BadRequest });
        }

        try
        {
            if (!string.IsNullOrEmpty(workingHours))
            {
                branchDto.WorkingHours = JsonSerializer.Deserialize<List<WorkingPeriodDTO>>(workingHours);
            }
            var updatedBranch = await _branchService.UpdateBranchAsync(id, branchDto,image);

            if (updatedBranch == null)
            {
                return NotFound(new { message = "Branch not found", statusCode = (int)HttpStatusCode.NotFound });
            }

            return Ok(new
            {
                message = "Branch updated successfully",
                statusCode = (int)HttpStatusCode.OK,
                data = updatedBranch
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to update branch", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBranch(Guid id)
    {
        try
        {
            var result = await _branchService.DeleteBranchAsync(id); // مرر الـ BranchId فقط

            if (!result)
            {
                return NotFound(new { message = "Branch not found", statusCode = (int)HttpStatusCode.NotFound });
            }

            return Ok(new { message = "Branch deleted successfully", statusCode = (int)HttpStatusCode.OK });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to delete branch", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
        }
    }
}




