namespace MusicAlbumsLibrary.Core.Entities;

public class Album
{
    public int Id { get; set; }

    public int LibraryId { get; set; }
    public Library Library { get; set; } = null!;
    public string MusicCatalogueProvider { get; set; } = null!;

    public string MusicCatalogueProviderAlbumId { get; set; } = null!;

    public string ArtistName { get; set; } = null!;

    public string AlbumName { get; set; } = null!;

    public string? CoverUrl { get; set; }

    public string AlbumUrl { get; set; } = null!;
}
