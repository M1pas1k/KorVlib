using BookLibrary.Application.DTOs;

namespace BookLibrary.Application.Services;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooks(int? userId = null);
    Task<BookDto?> GetBookById(int id, int? userId = null);
    Task<BookDto> AddBook(CreateBookDto bookDto);
    Task UpdateBook(int id, CreateBookDto bookDto);
    Task DeleteBook(int id);
}