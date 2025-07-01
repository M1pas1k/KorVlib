using BookLibrary.Core.Entities;
using System.Collections.Generic;

namespace BookLibrary.Domain.Entities;

public class User : IUser
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}