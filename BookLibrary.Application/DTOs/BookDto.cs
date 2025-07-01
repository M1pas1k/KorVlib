namespace BookLibrary.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public double? AverageRating { get; set; }
    public bool? IsRead { get; set; }
    public bool? WantToRead { get; set; }
}