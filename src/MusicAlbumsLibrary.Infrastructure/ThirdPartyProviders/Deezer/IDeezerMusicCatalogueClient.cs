namespace MusicAlbumsLibrary.Infrastructure.ThirdPartyProviders.Deezer;

public interface IDeezerMusicCatalogueClient
{
   Task<DeezerMusicCatalogueSearchResponse?> SearchAsync(string? albumName, string? artistName, CancellationToken cancellationToken);
}