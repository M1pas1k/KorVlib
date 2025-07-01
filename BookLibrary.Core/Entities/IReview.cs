namespace BookLibrary.Core.Entities;

public interface IReview
{
    int Id { get; set; }
    int BookId { get; set; }
    int UserId { get; set; }
    string Content { get; set; }
    int Rating { get; set; }
    DateTime CreatedAt { get; set; }
}