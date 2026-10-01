using System.Net.Http.Json;

namespace MusicAlbumsLibrary.Infrastructure.ThirdPartyProviders.Deezer;

public class DeezerMusicCatalogueClient : IDeezerMusicCatalogueClient
{
    private readonly HttpClient _client;

    public DeezerMusicCatalogueClient(HttpClient client)
        => _client = client;

    public async Task<DeezerMusicCatalogueSearchResponse?> SearchAsync(string? albumName, string? artistName, CancellationToken cancellationToken)
    {
        var searchUrl = BuildSearchUrl(albumName, artistName);

        var response = await _client.GetAsync(searchUrl, cancellationToken);

        response.EnsureSuccessStatusCode();
        var responsePayload = await response.Content.ReadFromJsonAsync<DeezerMusicCatalogueSearchResponse>(cancellationToken);

        return responsePayload;
    }

    private string BuildSearchUrl(string? albumName, string? artistName)
    {
        var searchTerms = new List<string>();

        if (!string.IsNullOrWhiteSpace(albumName))
        {
            searchTerms.Add($"album:\"{albumName}\"");
        }

        if (!string.IsNullOrWhiteSpace(artistName))
        {
            searchTerms.Add($"artist:\"{artistName}\"");
        }

        var queryString = Uri.EscapeDataString(string.Join(' ', searchTerms));
        return $"search/album?q={queryString}";
    }
}
