using Microsoft.Extensions.DependencyInjection;
using MusicAlbumsLibrary.Api.ApiModelContracts.User;
using MusicAlbumsLibrary.Application.Features.Albums.DTOs;
using MusicAlbumsLibrary.Infrastructure.Persistence;
using MusicAlbumsLibrary.IntegrationTests.Shared;
using System.Net;
using System.Net.Http.Json;

namespace MusicAlbumsLibrary.IntegrationTests.Features.Albums;


[Collection(nameof(SharedIntegrationTestCollection))]
public class SearchAlbumsIntegrationTests : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly MusicAlbumsLibraryDbContext _context;
    private readonly IServiceScope _scope;

    public SearchAlbumsIntegrationTests(IntegrationTestWebAppFactory factory)
    {
        _httpClient = factory.HttpClient;
        _scope = factory.Services.CreateScope();
        _context = _scope.ServiceProvider.GetService<MusicAlbumsLibraryDbContext>()!;
    }

    [Fact]
    public async Task SearchAlbumsApiEndpoint_ShouldReturnOkResponseWithAlbumsFromAllExternalProviders_WhenFiltersAreProvidedAndAlbumsExist()
    {
        // Arrange
        const string artistName = "Eminem";
        const string albumName = "The Eminem Show";

        // Act
        var result = await _httpClient.GetFromJsonAsync<List<AlbumSearchResultDTO>>
            ($"api/albums/search?albumName={albumName}&artistName={artistName}");

        // Assert
        Assert.NotEmpty(result);
        Assert.Collection<AlbumSearchResultDTO>(result,
            item =>
            {
                Assert.Equal("Deezer", item.MusicCatalogueProvider);
                Assert.Equal("103248", item.MusicCatalogueProviderAlbumId);
                Assert.Equal("Eminem", item.ArtistName);
                Assert.Equal("The Eminem Show", item.AlbumName);
                Assert.Equal("https://api.deezer.com/album/103248/image", item.CoverUrl);
                Assert.Equal("https://www.deezer.com/album/103248", item.AlbumUrl);
            },
            item =>
            {
                Assert.Equal("Deezer", item.MusicCatalogueProvider);
                Assert.Equal("14638278", item.MusicCatalogueProviderAlbumId);
                Assert.Equal("Eminem", item.ArtistName);
                Assert.Equal("The Eminem Show", item.AlbumName);
                Assert.Equal("https://api.deezer.com/album/14638278/image", item.CoverUrl);
                Assert.Equal("https://www.deezer.com/album/14638278", item.AlbumUrl);
            },
            item =>
            {
                Assert.Equal("Deezer", item.MusicCatalogueProvider);
                Assert.Equal("320098917", item.MusicCatalogueProviderAlbumId);
                Assert.Equal("Eminem", item.ArtistName);
                Assert.Equal("The Eminem Show (Expanded Edition)", item.AlbumName);
                Assert.Equal("https://api.deezer.com/album/320098917/image", item.CoverUrl);
                Assert.Equal("https://www.deezer.com/album/320098917", item.AlbumUrl);
            });
    }

    [Fact]
    public async Task SearchAlbumsApiEndpoint_ShouldReturnEmptyResult_WhenAlbumsDoNotMatchFilters()
    {
        // Arrange
        const string artistName = "test artist";
        const string albumName = "test album";

        // Act
        var result = await _httpClient.GetFromJsonAsync<List<AlbumSearchResultDTO>>
            ($"api/albums/search?albumName={albumName}&artistName={artistName}");

        // Assert
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _scope?.Dispose();
        _context?.Dispose();
    }
}