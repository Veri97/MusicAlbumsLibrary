using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicAlbumsLibrary.Infrastructure.Persistence;

namespace MusicAlbumsLibrary.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<MusicAlbumsLibraryDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("MusicAlbumsDbConnection"));
        });

        return services;
    }
}
