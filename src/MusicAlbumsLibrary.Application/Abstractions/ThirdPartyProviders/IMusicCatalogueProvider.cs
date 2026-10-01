using MusicAlbumsLibrary.Application.Features.Albums.DTOs;

namespace MusicAlbumsLibrary.Application.Abstractions.ThirdPartyProviders;

public interface IMusicCatalogueProvider
{
    string Name { get; }
    Task<List<AlbumSearchResultDTO>> SearchAsync(string? albumName, string? artistName, CancellationToken cancellationToken);
}