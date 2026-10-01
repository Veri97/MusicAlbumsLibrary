namespace MusicAlbumsLibrary.Infrastructure.ThirdPartyProviders.Deezer;

public sealed class DeezerMusicCatalogueSearchResponse
{
    public List<DeezerAlbum>? Data { get; init; }

    public int? Total { get; init; }

    public DeezerError? Error { get; init; }
}

public sealed class DeezerAlbum
{
    public long Id { get; init; }

    public string? Title { get; init; }

    public string? Link { get; init; }

    public string? Cover { get; init; }

    public DeezerArtist? Artist { get; init; }
}

public sealed class DeezerArtist
{
    public string? Name { get; init; }
}

public sealed class DeezerError
{
    public string? Type { get; init; }

    public string? Message { get; init; }

    public int Code { get; init; }
}