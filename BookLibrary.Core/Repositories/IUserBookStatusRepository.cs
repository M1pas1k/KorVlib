using BookLibrary.Core.Entities;

namespace BookLibrary.Core.Repositories;

public interface IUserBookStatusRepository
{
    Task<IUserBookStatus?> GetStatus(int userId, int bookId);
    Task UpdateStatus(IUserBookStatus status);
    Task AddStatus(IUserBookStatus status);
}