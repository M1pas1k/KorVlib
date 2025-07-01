namespace BookLibrary.Core.Entities;

public interface IUser
{
    int Id { get; set; }
    string Username { get; set; }
    string Email { get; set; }
    string PasswordHash { get; set; }
}