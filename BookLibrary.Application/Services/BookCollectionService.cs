using BookLibrary.Application.DTOs;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Repositories;
using BookLibrary.Domain.Entities;

namespace BookLibrary.Application.Services;

public class BookCollectionService : IBookCollectionService
{
    private readonly IBookCollectionRepository _collectionRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;

    public BookCollectionService(
        IBookCollectionRepository collectionRepository,
        IBookRepository bookRepository,
        IUserRepository userRepository)
    {
        _collectionRepository = collectionRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<BookCollectionDto>> GetUserCollections(int userId)
    {
        var collections = await _collectionRepository.GetByUserId(userId);
        return collections.Select(MapToBookCollectionDto);
    }

    public async Task<BookCollectionDto> GetCollectionById(int id, int userId)
    {
        var collection = await _collectionRepository.GetById(id);
        if (collection == null || collection.UserId != userId)
            throw new ArgumentException("Collection not found or access denied");

        var books = await _collectionRepository.GetBooksInCollection(collection.Id);
        var bookDtos = books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            ISBN = b.ISBN,
            Year = b.Year,
            Description = b.Description,
            CoverImageUrl = b.CoverImageUrl
        });

        var collectionDto = MapToBookCollectionDto(collection);
        collectionDto.Books = bookDtos;

        return collectionDto;
    }

    public async Task<BookCollectionDto> AddCollection(CreateBookCollectionDto collectionDto, int userId)
    {
        var collection = new BookCollection
        {
            Name = collectionDto.Name,
            Description = collectionDto.Description,
            UserId = userId
        };

        await _collectionRepository.Add(collection);
        return MapToBookCollectionDto(collection);
    }

    public async Task UpdateCollection(int id, CreateBookCollectionDto collectionDto, int userId)
    {
        var collection = await _collectionRepository.GetById(id);
        if (collection == null || collection.UserId != userId)
            throw new ArgumentException("Collection not found or access denied");

        collection.Name = collectionDto.Name;
        collection.Description = collectionDto.Description;

        await _collectionRepository.Update(collection);
    }

    public async Task DeleteCollection(int id, int userId)
    {
        var collection = await _collectionRepository.GetById(id);
        if (collection == null || collection.UserId != userId)
            throw new ArgumentException("Collection not found or access denied");

        await _collectionRepository.Delete(id);
    }

    public async Task AddBookToCollection(int collectionId, int bookId, int userId)
    {
        var collection = await _collectionRepository.GetById(collectionId);
        if (collection == null || collection.UserId != userId)
            throw new ArgumentException("Collection not found or access denied");

        var book = await _bookRepository.GetById(bookId);
        if (book == null) throw new ArgumentException("Book not found");

        await _collectionRepository.AddBookToCollection(collectionId, bookId);
    }

    public async Task RemoveBookFromCollection(int collectionId, int bookId, int userId)
    {
        var collection = await _collectionRepository.GetById(collectionId);
        if (collection == null || collection.UserId != userId)
            throw new ArgumentException("Collection not found or access denied");

        await _collectionRepository.RemoveBookFromCollection(collectionId, bookId);
    }

    private static BookCollectionDto MapToBookCollectionDto(IBookCollection collection)
    {
        return new BookCollectionDto
        {
            Id = collection.Id,
            Name = collection.Name,
            Description = collection.Description,
            UserId = collection.UserId
        };
    }
}