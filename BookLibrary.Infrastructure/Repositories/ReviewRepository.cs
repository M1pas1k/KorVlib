using BookLibrary.Core.Entities;
using BookLibrary.Core.Repositories;
using BookLibrary.Domain.Entities;
using BookLibrary.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IReview>> GetByBookId(int bookId)
    {
        return await _context.Reviews
            .Where(r => r.BookId == bookId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReview?> GetById(int id)
    {
        return await _context.Reviews.FindAsync(id);
    }

    public async Task Add(IReview review)
    {
        await _context.Reviews.AddAsync((Review)review);
        await _context.SaveChangesAsync();
    }

    public async Task Update(IReview review)
    {
        _context.Reviews.Update((Review)review);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<double> GetAverageRatingForBook(int bookId)
    {
        return await _context.Reviews
            .Where(r => r.BookId == bookId)
            .AverageAsync(r => (double)r.Rating);
    }
}