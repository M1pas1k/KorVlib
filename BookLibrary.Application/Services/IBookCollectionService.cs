using BookLibrary.Application.DTOs;

namespace BookLibrary.Application.Services;

public interface IBookCollectionService
{
    Task<IEnumerable<BookCollectionDto>> GetUserCollections(int userId);
    Task<BookCollectionDto> GetCollectionById(int id, int userId);
    Task<BookCollectionDto> AddCollection(CreateBookCollectionDto collectionDto, int userId);
    Task UpdateCollection(int id, CreateBookCollectionDto collectionDto, int userId);
    Task DeleteCollection(int id, int userId);
    Task AddBookToCollection(int collectionId, int bookId, int userId);
    Task RemoveBookFromCollection(int collectionId, int bookId, int userId);
}