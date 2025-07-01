namespace BookLibrary.Application.DTOs;

public class BookCollectionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
    public IEnumerable<BookDto>? Books { get; set; }
}