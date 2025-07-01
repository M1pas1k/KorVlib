using BookLibrary.Application.DTOs;

namespace BookLibrary.Application.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetBookReviews(int bookId);
    Task<ReviewDto> AddReview(CreateReviewDto reviewDto, int userId);
    Task UpdateReview(int id, CreateReviewDto reviewDto, int userId);
    Task DeleteReview(int id, int userId);
}