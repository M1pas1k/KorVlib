using BookLibrary.Core.Entities;
using BookLibrary.Core.Repositories;
using BookLibrary.Domain.Entities;
using BookLibrary.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IBook>> GetAll()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<IBook?> GetById(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task Add(IBook book)
    {
        await _context.Books.AddAsync((Book)book);
        await _context.SaveChangesAsync();
    }

    public async Task Update(IBook book)
    {
        _context.Books.Update((Book)book);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Books.AnyAsync(b => b.Id == id);
    }
}