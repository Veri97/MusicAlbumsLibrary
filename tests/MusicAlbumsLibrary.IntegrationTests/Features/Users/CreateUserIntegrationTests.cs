using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicAlbumsLibrary.Api.ApiModelContracts.User;
using MusicAlbumsLibrary.Infrastructure.Persistence;
using MusicAlbumsLibrary.IntegrationTests.Shared;
using System.Net;
using System.Net.Http.Json;

namespace MusicAlbumsLibrary.IntegrationTests.Features.Users;

[Collection(nameof(SharedIntegrationTestCollection))]
public class CreateUserIntegrationTests : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly MusicAlbumsLibraryDbContext _context;
    private readonly IServiceScope _scope;

    public CreateUserIntegrationTests(IntegrationTestWebAppFactory factory)
    {
        _httpClient = factory.HttpClient;
        _scope = factory.Services.CreateScope();
        _context = _scope.ServiceProvider.GetService<MusicAlbumsLibraryDbContext>()!;
    }

    [Fact]
    public async Task CreateUserApiEndpoint_ShouldReturnOkResponseWithUserData_WhenApiRequestIsValid()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Name = "User 1"
        };

        // Act
        var responseMessage = await _httpClient.PostAsJsonAsync("api/users", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);

        var createdUser = await _context.Users
                                        .Include(x => x.Library)
                                        .FirstOrDefaultAsync(x => x.Name == request.Name, CancellationToken.None);

        Assert.NotNull(createdUser);
        Assert.NotNull(createdUser.Library);
        Assert.Equal(request.Name, createdUser.Name);
        Assert.Equal(createdUser.Library.UserId, createdUser.Id);
    }

    public void Dispose()
    {
        _scope?.Dispose();
        _context?.Dispose();
    }
}