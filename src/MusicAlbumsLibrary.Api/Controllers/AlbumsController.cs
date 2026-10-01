using Microsoft.AspNetCore.Mvc;
using MusicAlbumsLibrary.Application.Abstractions.Features.Albums;
using MusicAlbumsLibrary.Application.Features.Albums.DTOs;

namespace MusicAlbumsLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumsService _albumsService;

    public AlbumsController(IAlbumsService albumsService)
    {
        _albumsService = albumsService;
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<AlbumSearchResultDTO>>> Search([FromQuery] string? albumName, [FromQuery] string? artistName,
        CancellationToken cancellationToken)
    {
        var albums = await _albumsService.SearchAsync(albumName, artistName, cancellationToken);
        return Ok(albums);
    }
}