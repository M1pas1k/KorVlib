using BookLibrary.Core.Entities;

namespace BookLibrary.Domain.Entities;

public class Book : IBook
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
}