namespace MusicAlbumsLibrary.Application.Features.Albums.DTOs;

public sealed class AlbumSearchResultDTO
{
    public string MusicCatalogueProvider { get; set; } = null!;

    public string MusicCatalogueProviderAlbumId { get; set; } = null!;
    public string ArtistName { get; set; } = null!;

    public string AlbumName { get; set; } = null!;

    public string? CoverUrl { get; set; }

    public string AlbumUrl { get; set; } = null!;
}
