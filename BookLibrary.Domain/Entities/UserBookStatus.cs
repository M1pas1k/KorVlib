using BookLibrary.Core.Entities;

namespace BookLibrary.Domain.Entities;

public class UserBookStatus : IUserBookStatus
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public bool IsRead { get; set; }
    public bool WantToRead { get; set; }
}