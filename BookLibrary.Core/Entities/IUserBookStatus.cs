namespace BookLibrary.Core.Entities;

public interface IUserBookStatus
{
    int Id { get; set; }
    int BookId { get; set; }
    int UserId { get; set; }
    bool IsRead { get; set; }
    bool WantToRead { get; set; }
}