using BookLibrary.Application.DTOs;

namespace BookLibrary.Application.Services;

public interface IUserBookStatusService
{
    Task<UserBookStatusDto> GetStatus(int bookId, int userId);
    Task<UserBookStatusDto> UpdateStatus(UserBookStatusDto statusDto, int userId);
}