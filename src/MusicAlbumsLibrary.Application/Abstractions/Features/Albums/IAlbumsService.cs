using MusicAlbumsLibrary.Application.Features.Albums.DTOs;

namespace MusicAlbumsLibrary.Application.Abstractions.Features.Albums;

public interface IAlbumsService
{
    Task<List<AlbumSearchResultDTO>> SearchAsync(string? albumName, string? artistName, CancellationToken cancellationToken);
}
