using BookLibrary.Application.DTOs;
using BookLibrary.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CollectionsController : ControllerBase
{
    private readonly IBookCollectionService _collectionService;

    public CollectionsController(IBookCollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookCollectionDto>>> GetUserCollections()
    {
        var userId = GetUserIdFromToken();
        var collections = await _collectionService.GetUserCollections(userId);
        return Ok(collections);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookCollectionDto>> GetCollectionById(int id)
    {
        var userId = GetUserIdFromToken();
        var collection = await _collectionService.GetCollectionById(id, userId);
        return Ok(collection);
    }

    [HttpPost]
    public async Task<ActionResult<BookCollectionDto>> AddCollection([FromBody] CreateBookCollectionDto collectionDto)
    {
        var userId = GetUserIdFromToken();
        var collection = await _collectionService.AddCollection(collectionDto, userId);
        return CreatedAtAction(nameof(GetCollectionById), new { id = collection.Id }, collection);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCollection(int id, [FromBody] CreateBookCollectionDto collectionDto)
    {
        var userId = GetUserIdFromToken();
        await _collectionService.UpdateCollection(id, collectionDto, userId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCollection(int id)
    {
        var userId = GetUserIdFromToken();
        await _collectionService.DeleteCollection(id, userId);
        return NoContent();
    }

    [HttpPost("{collectionId}/books/{bookId}")]
    public async Task<IActionResult> AddBookToCollection(int collectionId, int bookId)
    {
        var userId = GetUserIdFromToken();
        await _collectionService.AddBookToCollection(collectionId, bookId, userId);
        return NoContent();
    }

    [HttpDelete("{collectionId}/books/{bookId}")]
    public async Task<IActionResult> RemoveBookFromCollection(int collectionId, int bookId)
    {
        var userId = GetUserIdFromToken();
        await _collectionService.RemoveBookFromCollection(collectionId, bookId, userId);
        return NoContent();
    }

    private int GetUserIdFromToken()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }
        throw new UnauthorizedAccessException("Invalid user ID in token");
    }
}