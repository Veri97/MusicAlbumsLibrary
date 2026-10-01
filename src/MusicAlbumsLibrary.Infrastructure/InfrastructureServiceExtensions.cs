using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MusicAlbumsLibrary.Application.Abstractions.ThirdPartyProviders;
using MusicAlbumsLibrary.Core.Contracts;
using MusicAlbumsLibrary.Infrastructure.Persistence;
using MusicAlbumsLibrary.Infrastructure.Persistence.Repositories;
using MusicAlbumsLibrary.Infrastructure.ThirdPartyProviders;
using MusicAlbumsLibrary.Infrastructure.ThirdPartyProviders.Deezer;

namespace MusicAlbumsLibrary.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<MusicAlbumsLibraryDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("MusicAlbumsDbConnection"));
        });

        services.AddScoped<IMusicAlbumsLibraryRepository, MusicAlbumsLibraryRepository>();


        services.Configure<DeezerMusicCatalogueSettings>(
           options => config.GetSection(DeezerMusicCatalogueSettings.Name).Bind(options));

        services.AddHttpClient<IDeezerMusicCatalogueClient, DeezerMusicCatalogueClient>((serviceProvider, client) =>
        {
            var settings = serviceProvider
                .GetRequiredService<IOptions<DeezerMusicCatalogueSettings>>().Value;

            client.BaseAddress = new Uri(settings.BaseUrl);
        });

        services.AddScoped<IMusicCatalogueProvider, DeezerMusicCatalogueProvider>();

        return services;
    }
}
