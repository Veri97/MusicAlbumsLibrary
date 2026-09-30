namespace MusicAlbumsLibrary.Application.Features.Libraries.DTOs;

public sealed class CreatedAlbumDto
{
    public int Id { get; init; }

    public int LibraryId { get; init; }

    public string MusicCatalogueProvider { get; init; } = null!;

    public string MusicCatalogueProviderAlbumId { get; init; } = null!;

    public string ArtistName { get; init; } = null!;

    public string AlbumName { get; init; } = null!;

    public string? CoverUrl { get; init; }

    public string AlbumUrl { get; init; } = null!;
}
