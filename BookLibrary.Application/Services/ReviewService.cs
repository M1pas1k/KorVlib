using BookLibrary.Application.DTOs;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Repositories;
using BookLibrary.Domain.Entities;

namespace BookLibrary.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;

    public ReviewService(
        IReviewRepository reviewRepository,
        IBookRepository bookRepository,
        IUserRepository userRepository)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ReviewDto>> GetBookReviews(int bookId)
    {
        var reviews = await _reviewRepository.GetByBookId(bookId);
        var reviewDtos = new List<ReviewDto>();

        foreach (var review in reviews)
        {
            var user = await _userRepository.GetById(review.UserId);
            reviewDtos.Add(MapToReviewDto(review, user));
        }

        return reviewDtos;
    }

    public async Task<ReviewDto> AddReview(CreateReviewDto reviewDto, int userId)
    {
        var book = await _bookRepository.GetById(reviewDto.BookId);
        if (book == null) throw new ArgumentException("Book not found");

        var review = new Review
        {
            BookId = reviewDto.BookId,
            UserId = userId,
            Content = reviewDto.Content,
            Rating = reviewDto.Rating,
            CreatedAt = DateTime.UtcNow
        };

        await _reviewRepository.Add(review);

        var user = await _userRepository.GetById(userId);
        return MapToReviewDto(review, user);
    }

    public async Task UpdateReview(int id, CreateReviewDto reviewDto, int userId)
    {
        var review = await _reviewRepository.GetById(id);
        if (review == null || review.UserId != userId)
            throw new ArgumentException("Review not found or access denied");

        review.Content = reviewDto.Content;
        review.Rating = reviewDto.Rating;

        await _reviewRepository.Update(review);
    }

    public async Task DeleteReview(int id, int userId)
    {
        var review = await _reviewRepository.GetById(id);
        if (review == null || review.UserId != userId)
            throw new ArgumentException("Review not found or access denied");

        await _reviewRepository.Delete(id);
    }

    private static ReviewDto MapToReviewDto(IReview review, IUser? user)
    {
        return new ReviewDto
        {
            Id = review.Id,
            BookId = review.BookId,
            UserId = review.UserId,
            UserName = user?.Username ?? "Unknown",
            Content = review.Content,
            Rating = review.Rating,
            CreatedAt = review.CreatedAt
        };
    }
}