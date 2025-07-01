namespace BookLibrary.Core.Entities;

public interface IBook
{
    int Id { get; set; }
    string Title { get; set; }
    string Author { get; set; }
    string ISBN { get; set; }
    int? Year { get; set; }
    string? Description { get; set; }
    string? CoverImageUrl { get; set; }
}