using Microsoft.AspNetCore.Mvc;
using MusicAlbumsLibrary.Api.ApiModelContracts.Library;
using MusicAlbumsLibrary.Application.Abstractions.Features.Libraries;
using MusicAlbumsLibrary.Application.Features.Libraries.DTOs;

namespace MusicAlbumsLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibrariesController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public LibrariesController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpPost("{id}/albums")]
    public async Task<ActionResult<List<CreatedAlbumDto>>> AddAlbumToLibrary([FromRoute] int id,
        [FromBody] AddAlbumsToLibraryRequest request, CancellationToken cancellationToken)
    {
        var albums = request.Albums.Select(x => new AlbumDto
        {
            MusicCatalogueProvider = x.MusicCatalogueProvider,
            MusicCatalogueProviderAlbumId = x.MusicCatalogueProviderAlbumId,
            ArtistName = x.ArtistName,
            AlbumName = x.AlbumName,
            CoverUrl = x.CoverUrl,
            AlbumUrl = x.AlbumUrl
        }).ToList();

        var result = await _libraryService.AddAlbumsToLibraryAsync(
            id, albums, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("albums/{id}")]
    public async Task<ActionResult<List<CreatedAlbumDto>>> DeleteAlbum([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _libraryService.DeleteAlbumFromLibraryAsync(id, cancellationToken);

        return NoContent();
    }
}
