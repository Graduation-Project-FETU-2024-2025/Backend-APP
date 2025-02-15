using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace medical_app_db.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BranchController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    //GET: api/Branch
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllBranches(int page = 1, int pageSize = 3)
    {
        var lang = Request.Headers["lang"].ToString().ToLower();

        if (string.IsNullOrEmpty(lang))
        {
            return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
        }

        var branches = await _branchService.GetAllBranchesAsync(page, pageSize);

        if (branches == null || !branches.Any())
        {
            return NoContent(); 
        }

        var result = branches.Select(b => new
        {
            Id = b.Id,
            PharmacyId = b.PharmacyId,
            BranchName = lang == "ar" ? b.AR_BranchName : b.EN_BranchName,
            PhoneNumber = b.PhoneNumber,
            Image = b.Image,
            Status = b.Status,
            Lat = b.Lat,
            Long = b.Long,
            WorkingHours = b.WorkingHours
        });

        return Ok(new { message = "Success", statusCode = (int)HttpStatusCode.OK, data = result });
    }

    // api/Branch/{id}
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranchById(Guid id)
    {
        var lang = Request.Headers["lang"].ToString().ToLower();

        if (string.IsNullOrEmpty(lang))
        {
            return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
        }

        try
        {
            var branch = await _branchService.GetBranchByIdAsync(id,lang);

            if (branch == null)
            {
                return NotFound(new { message = "Branch not found", statusCode = (int)HttpStatusCode.NotFound });
            }

            var result = new
            {
                Id = branch.Id,
                PharmacyId = branch.PharmacyId,
                BranchName = lang == "ar" ? branch.AR_BranchName : branch.EN_BranchName,
                PhoneNumber = branch.PhoneNumber,
                Image = branch.Image,
                Status = branch.Status,
                Lat = branch.Lat,
                Long = branch.Long,
                WorkingHours = branch.WorkingHours
            };

            return Ok(new { message = "Success", statusCode = (int)HttpStatusCode.OK, data = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
        }
    }

    // POST: api/Branch
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddBranch(BranchDTO branchDto)
    {
        if (branchDto == null)
        {
            return BadRequest(new { message = "Invalid branch data", statusCode = (int)HttpStatusCode.BadRequest });
        }

        try
        {
            var createdBranch = await _branchService.AddBranchAsync(branchDto);

            return CreatedAtAction(nameof(GetBranchById), new { id = createdBranch.Id }, new
            {
                message = "Branch created successfully",
                statusCode = (int)HttpStatusCode.Created,
                data = createdBranch
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to create branch", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
        }
    }

    // PUT: api/Branch/{id}
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBranch(Guid id, BranchDTO branchDto)
    {
        if (branchDto == null)
        {
            return BadRequest(new { message = "Invalid branch data", statusCode = (int)HttpStatusCode.BadRequest });
        }

        try
        {
            var updatedBranch = await _branchService.UpdateBranchAsync(id, branchDto);

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

    // DELETE: api/Branch/{id}
    [Authorize]
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


