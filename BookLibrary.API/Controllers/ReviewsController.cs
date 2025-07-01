using BookLibrary.Application.DTOs;
using BookLibrary.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("book/{bookId}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetBookReviews(int bookId)
    {
        var reviews = await _reviewService.GetBookReviews(bookId);
        return Ok(reviews);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ReviewDto>> AddReview([FromBody] CreateReviewDto reviewDto)
    {
        var userId = GetUserIdFromToken();
        var review = await _reviewService.AddReview(reviewDto, userId);
        return CreatedAtAction(nameof(GetBookReviews), new { bookId = review.BookId }, review);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateReview(int id, [FromBody] CreateReviewDto reviewDto)
    {
        var userId = GetUserIdFromToken();
        await _reviewService.UpdateReview(id, reviewDto, userId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var userId = GetUserIdFromToken();
        await _reviewService.DeleteReview(id, userId);
        return NoContent();
    }

    private int GetUserIdFromToken()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }
        throw new UnauthorizedAccessException("Invalid user ID in token");
    }
}