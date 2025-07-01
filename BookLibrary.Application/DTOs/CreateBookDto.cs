namespace BookLibrary.Application.DTOs;

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
}