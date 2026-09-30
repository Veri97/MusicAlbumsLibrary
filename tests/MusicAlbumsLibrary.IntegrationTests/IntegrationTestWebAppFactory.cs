using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MusicAlbumsLibrary.Api;
using MusicAlbumsLibrary.Infrastructure.Persistence;
using Testcontainers.MsSql;

namespace MusicAlbumsLibrary.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<IApiAssemblyMarker>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
           .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
           .WithPortBinding(1433)
           .WithEnvironment("ACCEPT_EULA", "Y")
           .WithPassword("PasswordTest1234!!#")
           .WithAutoRemove(true)
           .WithCleanUp(true)
           .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(1433))
           .Build();

    public HttpClient HttpClient { get; private set; } = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<MusicAlbumsLibraryDbContext>));
            services.RemoveAll(typeof(IConnectionFactory));

            services.AddDbContext<MusicAlbumsLibraryDbContext>(options =>
            {
                var connString = _dbContainer.GetConnectionString();
                options.UseSqlServer(connString);
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        HttpClient = CreateClient();
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetService<MusicAlbumsLibraryDbContext>();

        await context!.Database.EnsureCreatedAsync();
        await CleanDatabase(context!);
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }

    private async Task CleanDatabase(MusicAlbumsLibraryDbContext context)
    {
        await context.Users.ExecuteDeleteAsync();
    }
}
