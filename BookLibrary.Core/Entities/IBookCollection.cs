namespace BookLibrary.Core.Entities;

public interface IBookCollection
{
    int Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    int UserId { get; set; }
}