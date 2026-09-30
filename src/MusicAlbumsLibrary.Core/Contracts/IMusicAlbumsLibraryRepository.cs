using MusicAlbumsLibrary.Core.Entities;

namespace MusicAlbumsLibrary.Core.Contracts;

public interface IMusicAlbumsLibraryRepository
{
    Task CreateUserAsync(User user, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
