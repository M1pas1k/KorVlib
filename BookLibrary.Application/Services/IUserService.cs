using BookLibrary.Application.DTOs;

namespace BookLibrary.Application.Services;

public interface IUserService
{
    Task<UserDto> RegisterUser(RegisterUserDto registerUserDto);
    Task<UserDto?> LoginUser(LoginUserDto loginUserDto);
    Task<UserDto> GetUserById(int id);
}