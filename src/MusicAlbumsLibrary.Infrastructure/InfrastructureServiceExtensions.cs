using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicAlbumsLibrary.Core.Contracts;
using MusicAlbumsLibrary.Infrastructure.Persistence;
using MusicAlbumsLibrary.Infrastructure.Persistence.Repositories;

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

        return services;
    }
}
