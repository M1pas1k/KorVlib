namespace BookLibrary.Application.DTOs;

public class UserBookStatusDto
{
    public int BookId { get; set; }
    public bool IsRead { get; set; }
    public bool WantToRead { get; set; }
}