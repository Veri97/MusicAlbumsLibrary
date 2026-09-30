namespace MusicAlbumsLibrary.Api.ApiModelContracts.Library;

public sealed class AddAlbumsToLibraryRequest
{
    public List<AlbumRequest> Albums { get; init; } = new();
}

public sealed class AlbumRequest
{
    public string MusicCatalogueProvider { get; init; } = null!;

    public string MusicCatalogueProviderAlbumId { get; init; } = null!;

    public string ArtistName { get; init; } = null!;

    public string AlbumName { get; init; } = null!;

    public string? CoverUrl { get; init; }

    public string AlbumUrl { get; init; } = null!;
}