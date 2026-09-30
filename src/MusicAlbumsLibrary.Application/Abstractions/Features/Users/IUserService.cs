using MusicAlbumsLibrary.Application.Features.Users.DTOs;

namespace MusicAlbumsLibrary.Application.Abstractions.Features.Users;

public interface IUserService
{
    Task<CreatedUserDto> CreateUserAsync(string name, CancellationToken cancellationToken);
}