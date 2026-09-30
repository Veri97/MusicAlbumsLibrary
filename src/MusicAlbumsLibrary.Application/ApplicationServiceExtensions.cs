using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicAlbumsLibrary.Application.Abstractions.Features.Libraries;
using MusicAlbumsLibrary.Application.Abstractions.Features.Users;
using MusicAlbumsLibrary.Application.Features.Libraries;
using MusicAlbumsLibrary.Application.Features.Users;

namespace MusicAlbumsLibrary.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILibraryService, LibraryService>();
        return services;
    }
}