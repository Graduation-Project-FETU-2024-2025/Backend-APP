public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(Guid userId, int page, int pageSize);
    Task<Guid> CreateReviewAsync(Guid userId, AddReviewDto dto);
    Task UpdateReviewAsync(Guid userId, Guid reviewId, UpdateReviewDto dto);
    Task DeleteReviewAsync(Guid userId, Guid reviewId);
}


