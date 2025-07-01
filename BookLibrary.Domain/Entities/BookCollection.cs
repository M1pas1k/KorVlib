using BookLibrary.Core.Entities;

namespace BookLibrary.Domain.Entities;

public class BookCollection : IBookCollection
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
}