using BookLibrary.Application.DTOs;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Repositories;
using BookLibrary.Domain.Entities;
using BookLibrary.Infrastructure.Services;

namespace BookLibrary.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> RegisterUser(RegisterUserDto registerUserDto)
    {
        if (await _userRepository.EmailExists(registerUserDto.Email))
            throw new ArgumentException("Email already exists");

        var user = new User
        {
            Username = registerUserDto.Username,
            Email = registerUserDto.Email,
            PasswordHash = PasswordHasher.HashPassword(registerUserDto.Password)
        };

        await _userRepository.Add(user);
        return MapToUserDto(user);
    }

    public async Task<UserDto?> LoginUser(LoginUserDto loginUserDto)
    {
        var user = await _userRepository.GetByEmail(loginUserDto.Email);
        if (user == null) return null;

        if (!PasswordHasher.VerifyPassword(loginUserDto.Password, user.PasswordHash))
            return null;

        return MapToUserDto(user);
    }

    public async Task<UserDto> GetUserById(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null) throw new ArgumentException("User not found");
        return MapToUserDto(user);
    }

    private static UserDto MapToUserDto(IUser user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }
}