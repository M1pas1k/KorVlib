using BookLibrary.Core.Entities;
using BookLibrary.Core.Repositories;
using BookLibrary.Domain.Entities;
using BookLibrary.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Repositories;

public class BookCollectionRepository : IBookCollectionRepository
{
    private readonly ApplicationDbContext _context;

    public BookCollectionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IBookCollection>> GetByUserId(int userId)
    {
        return await _context.BookCollections
            .Where(bc => bc.UserId == userId)
            .ToListAsync();
    }

    public async Task<IBookCollection?> GetById(int id)
    {
        return await _context.BookCollections.FindAsync(id);
    }

    public async Task Add(IBookCollection collection)
    {
        await _context.BookCollections.AddAsync((BookCollection)collection);
        await _context.SaveChangesAsync();
    }

    public async Task Update(IBookCollection collection)
    {
        _context.BookCollections.Update((BookCollection)collection);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var collection = await _context.BookCollections.FindAsync(id);
        if (collection != null)
        {
            _context.BookCollections.Remove(collection);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddBookToCollection(int collectionId, int bookId)
    {
        var collection = await _context.BookCollections
            .Include(bc => bc.Books)
            .FirstOrDefaultAsync(bc => bc.Id == collectionId);

        var book = await _context.Books.FindAsync(bookId);

        if (collection != null && book != null)
        {
            collection.Books.Add(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveBookFromCollection(int collectionId, int bookId)
    {
        var collection = await _context.BookCollections
            .Include(bc => bc.Books)
            .FirstOrDefaultAsync(bc => bc.Id == collectionId);

        var book = collection?.Books.FirstOrDefault(b => b.Id == bookId);

        if (collection != null && book != null)
        {
            collection.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<IBook>> GetBooksInCollection(int collectionId)
    {
        var collection = await _context.BookCollections
            .Include(bc => bc.Books)
            .FirstOrDefaultAsync(bc => bc.Id == collectionId);

        return collection?.Books ?? Enumerable.Empty<Book>();
    }
}