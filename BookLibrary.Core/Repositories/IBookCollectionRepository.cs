using BookLibrary.Core.Entities;

namespace BookLibrary.Core.Repositories;

public interface IBookCollectionRepository
{
    Task<IEnumerable<IBookCollection>> GetByUserId(int userId);
    Task<IBookCollection?> GetById(int id);
    Task Add(IBookCollection collection);
    Task Update(IBookCollection collection);
    Task Delete(int id);
    Task AddBookToCollection(int collectionId, int bookId);
    Task RemoveBookFromCollection(int collectionId, int bookId);
    Task<IEnumerable<IBook>> GetBooksInCollection(int collectionId);
}