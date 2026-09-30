using MusicAlbumsLibrary.Application.Abstractions.Features.Users;
using MusicAlbumsLibrary.Application.Features.Users.DTOs;
using MusicAlbumsLibrary.Core.Contracts;
using MusicAlbumsLibrary.Core.Entities;

namespace MusicAlbumsLibrary.Application.Features.Users;

public class UserService : IUserService
{
    private readonly IMusicAlbumsLibraryRepository _musicAlbumsLibraryRepository;

    public UserService(IMusicAlbumsLibraryRepository musicAlbumsLibraryRepository)
    {
        _musicAlbumsLibraryRepository = musicAlbumsLibraryRepository;
    }

    public async Task<CreatedUserDto> CreateUserAsync(string name, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = name
        };

        await _musicAlbumsLibraryRepository.CreateUserAsync(user, cancellationToken);
        await _musicAlbumsLibraryRepository.SaveChangesAsync(cancellationToken);

        return new CreatedUserDto
        {
            Id = user.Id,
            Name = user.Name
        };
    }
}