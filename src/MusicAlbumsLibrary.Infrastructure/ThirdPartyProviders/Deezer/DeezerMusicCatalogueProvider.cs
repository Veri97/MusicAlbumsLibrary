using MusicAlbumsLibrary.Application.Abstractions.ThirdPartyProviders;
using MusicAlbumsLibrary.Application.Features.Albums.DTOs;

namespace MusicAlbumsLibrary.Infrastructure.ThirdPartyProviders.Deezer;

public sealed class DeezerMusicCatalogueProvider : IMusicCatalogueProvider
{
    public string Name => MusicCatalogueProviders.Deezer;

    private readonly IDeezerMusicCatalogueClient _deezerMusicCatalogueClient;

    public DeezerMusicCatalogueProvider(IDeezerMusicCatalogueClient deezerMusicCatalogueClient)
    {
        _deezerMusicCatalogueClient = deezerMusicCatalogueClient;
    }

    public async Task<List<AlbumSearchResultDTO>> SearchAsync(string? albumName, string? artistName, CancellationToken cancellationToken)
    {
        var response = await _deezerMusicCatalogueClient.SearchAsync(albumName, artistName, cancellationToken);

        var mappedResult = response?.Data?.Select(x => new AlbumSearchResultDTO
        {
            MusicCatalogueProvider = Name,
            MusicCatalogueProviderAlbumId = x.Id.ToString(),
            ArtistName = x?.Artist?.Name,
            AlbumName = x.Title,
            CoverUrl = x.Cover,
            AlbumUrl = x.Link
        }).ToList();

        return mappedResult;
    }
}