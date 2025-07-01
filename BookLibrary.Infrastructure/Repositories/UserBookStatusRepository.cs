using BookLibrary.Core.Entities;
using BookLibrary.Core.Repositories;
using BookLibrary.Domain.Entities;
using BookLibrary.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Repositories;

public class UserBookStatusRepository : IUserBookStatusRepository
{
    private readonly ApplicationDbContext _context;

    public UserBookStatusRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IUserBookStatus?> GetStatus(int userId, int bookId)
    {
        return await _context.UserBookStatuses
            .FirstOrDefaultAsync(ubs => ubs.UserId == userId && ubs.BookId == bookId);
    }

    public async Task UpdateStatus(IUserBookStatus status)
    {
        _context.UserBookStatuses.Update((UserBookStatus)status);
        await _context.SaveChangesAsync();
    }

    public async Task AddStatus(IUserBookStatus status)
    {
        await _context.UserBookStatuses.AddAsync((UserBookStatus)status);
        await _context.SaveChangesAsync();
    }
}