using BookLibrary.Application.DTOs;
using BookLibrary.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StatusController : ControllerBase
{
    private readonly IUserBookStatusService _statusService;

    public StatusController(IUserBookStatusService statusService)
    {
        _statusService = statusService;
    }

    [HttpGet("{bookId}")]
    public async Task<ActionResult<UserBookStatusDto>> GetStatus(int bookId)
    {
        var userId = GetUserIdFromToken();
        var status = await _statusService.GetStatus(bookId, userId);
        return Ok(status);
    }

    [HttpPut]
    public async Task<ActionResult<UserBookStatusDto>> UpdateStatus([FromBody] UserBookStatusDto statusDto)
    {
        var userId = GetUserIdFromToken();
        var status = await _statusService.UpdateStatus(statusDto, userId);
        return Ok(status);
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