using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    private Guid GetUserIdFromToken()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userIdClaim!);
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews(int page = 1, int pageSize = 10)
    {
        var userId = GetUserIdFromToken();
        var reviews = await _reviewService.GetUserReviewsAsync(userId, page, pageSize);
        return Ok(new { data = reviews, Messgae ="Success" , Status = HttpStatusCode.OK});
    }
    [HttpGet("{clinicId}")]
    public async Task<IActionResult> GetReviewForClinic(Guid clinicId,int page = 1, int pageSize = 10)
    {
        var reviews = await _reviewService.GetClinicReviewsAsync(clinicId, page, pageSize);
        return Ok(new { data = reviews, Messgae = "Success", Status = HttpStatusCode.OK });
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddReviewDto dto)
    {
        var userId = GetUserIdFromToken();
        var reviewId = await _reviewService.CreateReviewAsync(userId, dto);
        return Ok(new { message = "Review added" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReviewDto dto)
    {
        var userId = GetUserIdFromToken();
        await _reviewService.UpdateReviewAsync(userId, id, dto);
        return Ok(new { message = "Review updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var userId = GetUserIdFromToken();
            await _reviewService.DeleteReviewAsync(userId, id);
            return Ok(new { message = "Review deleted successfully" });
        }
        catch (UnauthorizedAccessException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to delete review", details = ex.Message });
        }
    }
}
