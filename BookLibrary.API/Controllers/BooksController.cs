using BookLibrary.Application.DTOs;
using BookLibrary.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
    {
        var userId = GetUserIdFromToken();
        var books = await _bookService.GetAllBooks(userId);
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBookById(int id)
    {
        var userId = GetUserIdFromToken();
        var book = await _bookService.GetBookById(id, userId);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BookDto>> AddBook([FromBody] CreateBookDto bookDto)
    {
        var book = await _bookService.AddBook(bookDto);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] CreateBookDto bookDto)
    {
        await _bookService.UpdateBook(id, bookDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteBook(id);
        return NoContent();
    }

    private int? GetUserIdFromToken()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }
        return null;
    }
}