using Microsoft.EntityFrameworkCore;
using MusicAlbumsLibrary.Infrastructure.Persistence;

namespace MusicAlbumsLibrary.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using MusicAlbumsLibraryDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<MusicAlbumsLibraryDbContext>();

        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

        try
        {
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogCritical(ex, "An error ocurred during migration!");
        }
    }
}
