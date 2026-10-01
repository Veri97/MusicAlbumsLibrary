using MusicAlbumsLibrary.Application.Abstractions.Features.Albums;
using MusicAlbumsLibrary.Application.Abstractions.ThirdPartyProviders;
using MusicAlbumsLibrary.Application.Features.Albums.DTOs;

namespace MusicAlbumsLibrary.Application.Features.Albums;

public class AlbumService : IAlbumsService
{
    private readonly List<IMusicCatalogueProvider> _musicCatalogueProviders;

    public AlbumService(IEnumerable<IMusicCatalogueProvider> musicCatalogueProviders)
	{
        _musicCatalogueProviders = musicCatalogueProviders.ToList();
    }

    public async Task<List<AlbumSearchResultDTO>> SearchAsync(string? albumName, string? artistName, CancellationToken cancellationToken)
    {
        // Merged results from all providers
        var results = await Task.WhenAll(_musicCatalogueProviders
            .Select(x => x.SearchAsync(albumName, artistName, cancellationToken)));

        return results.SelectMany(x => x).ToList();
    }
}
