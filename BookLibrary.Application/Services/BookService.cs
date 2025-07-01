using BookLibrary.Application.DTOs;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Repositories;
using BookLibrary.Domain.Entities;

namespace BookLibrary.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IUserBookStatusRepository _userBookStatusRepository;

    public BookService(
        IBookRepository bookRepository,
        IReviewRepository reviewRepository,
        IUserBookStatusRepository userBookStatusRepository)
    {
        _bookRepository = bookRepository;
        _reviewRepository = reviewRepository;
        _userBookStatusRepository = userBookStatusRepository;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooks(int? userId = null)
    {
        var books = await _bookRepository.GetAll();
        var bookDtos = new List<BookDto>();

        foreach (var book in books)
        {
            var averageRating = await _reviewRepository.GetAverageRatingForBook(book.Id);
            var bookDto = MapToBookDto(book, averageRating);

            if (userId.HasValue)
            {
                var status = await _userBookStatusRepository.GetStatus(userId.Value, book.Id);
                if (status != null)
                {
                    bookDto.IsRead = status.IsRead;
                    bookDto.WantToRead = status.WantToRead;
                }
            }

            bookDtos.Add(bookDto);
        }

        return bookDtos;
    }

    public async Task<BookDto?> GetBookById(int id, int? userId = null)
    {
        var book = await _bookRepository.GetById(id);
        if (book == null) return null;

        var averageRating = await _reviewRepository.GetAverageRatingForBook(book.Id);
        var bookDto = MapToBookDto(book, averageRating);

        if (userId.HasValue)
        {
            var status = await _userBookStatusRepository.GetStatus(userId.Value, book.Id);
            if (status != null)
            {
                bookDto.IsRead = status.IsRead;
                bookDto.WantToRead = status.WantToRead;
            }
        }

        return bookDto;
    }

    public async Task<BookDto> AddBook(CreateBookDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            Author = bookDto.Author,
            ISBN = bookDto.ISBN,
            Year = bookDto.Year,
            Description = bookDto.Description,
            CoverImageUrl = bookDto.CoverImageUrl
        };

        await _bookRepository.Add(book);
        return MapToBookDto(book, null);
    }

    public async Task UpdateBook(int id, CreateBookDto bookDto)
    {
        var book = await _bookRepository.GetById(id);
        if (book == null) throw new ArgumentException("Book not found");

        book.Title = bookDto.Title;
        book.Author = bookDto.Author;
        book.ISBN = bookDto.ISBN;
        book.Year = bookDto.Year;
        book.Description = bookDto.Description;
        book.CoverImageUrl = bookDto.CoverImageUrl;

        await _bookRepository.Update(book);
    }

    public async Task DeleteBook(int id)
    {
        await _bookRepository.Delete(id);
    }

    private static BookDto MapToBookDto(IBook book, double? averageRating)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Year = book.Year,
            Description = book.Description,
            CoverImageUrl = book.CoverImageUrl,
            AverageRating = averageRating
        };
    }
}