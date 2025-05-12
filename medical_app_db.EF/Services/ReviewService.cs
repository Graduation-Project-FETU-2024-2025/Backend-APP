using medical_app_db.Core.Models;
using medical_app_db.EF.Data;
using Microsoft.EntityFrameworkCore;

public class ReviewService : IReviewService
{
    private readonly MedicalDbContext _context;

    public ReviewService(MedicalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(Guid userId, int page, int pageSize)
    {
        return await _context.Reviews
            .Where(r => r.UserId == userId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                ClinicId = r.ClinicId,
                Rate = r.Rate,
                Comment = r.Comment
            })
            .ToListAsync();
    }

    public async Task<Guid> CreateReviewAsync(Guid userId, AddReviewDto dto)
    {
        var review = new Review
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ClinicId = dto.ClinicId,
            Rate = dto.Rate,
            Comment = dto.Comment
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return review.Id;
    }

    public async Task UpdateReviewAsync(Guid userId, Guid reviewId, UpdateReviewDto dto)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == reviewId && r.UserId == userId);

        if (review == null)
            throw new UnauthorizedAccessException("Review not found or access denied.");

        review.Rate = dto.Rate;
        review.Comment = dto.Comment;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteReviewAsync(Guid userId, Guid reviewId)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == reviewId && r.UserId == userId);

        if (review == null)
            throw new UnauthorizedAccessException("Review not found or access denied.");

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
    }
}

