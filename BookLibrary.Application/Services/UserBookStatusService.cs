using BookLibrary.Application.DTOs;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Repositories;
using BookLibrary.Domain.Entities;

namespace BookLibrary.Application.Services;

public class UserBookStatusService : IUserBookStatusService
{
    private readonly IUserBookStatusRepository _userBookStatusRepository;
    private readonly IBookRepository _bookRepository;

    public UserBookStatusService(
        IUserBookStatusRepository userBookStatusRepository,
        IBookRepository bookRepository)
    {
        _userBookStatusRepository = userBookStatusRepository;
        _bookRepository = bookRepository;
    }

    public async Task<UserBookStatusDto> GetStatus(int bookId, int userId)
    {
        var book = await _bookRepository.GetById(bookId);
        if (book == null) throw new ArgumentException("Book not found");

        var status = await _userBookStatusRepository.GetStatus(userId, bookId);

        return new UserBookStatusDto
        {
            BookId = bookId,
            IsRead = status?.IsRead ?? false,
            WantToRead = status?.WantToRead ?? false
        };
    }

    public async Task<UserBookStatusDto> UpdateStatus(UserBookStatusDto statusDto, int userId)
    {
        var book = await _bookRepository.GetById(statusDto.BookId);
        if (book == null) throw new ArgumentException("Book not found");

        var existingStatus = await _userBookStatusRepository.GetStatus(userId, statusDto.BookId);

        if (existingStatus == null)
        {
            var newStatus = new UserBookStatus
            {
                BookId = statusDto.BookId,
                UserId = userId,
                IsRead = statusDto.IsRead,
                WantToRead = statusDto.WantToRead
            };
            await _userBookStatusRepository.AddStatus(newStatus);
        }
        else
        {
            existingStatus.IsRead = statusDto.IsRead;
            existingStatus.WantToRead = statusDto.WantToRead;
            await _userBookStatusRepository.UpdateStatus(existingStatus);
        }

        return statusDto;
    }
}