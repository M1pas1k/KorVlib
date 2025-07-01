namespace BookLibrary.Application.DTOs;

public class ReviewDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}