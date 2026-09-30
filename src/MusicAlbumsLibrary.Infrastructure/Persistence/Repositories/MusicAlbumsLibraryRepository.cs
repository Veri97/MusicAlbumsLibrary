using MusicAlbumsLibrary.Core.Contracts;
using MusicAlbumsLibrary.Core.Entities;

namespace MusicAlbumsLibrary.Infrastructure.Persistence.Repositories;

public class MusicAlbumsLibraryRepository : IMusicAlbumsLibraryRepository
{
    private readonly MusicAlbumsLibraryDbContext _dbContext;

    public MusicAlbumsLibraryRepository(MusicAlbumsLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        user.Library = new Library { UserId = user.Id };
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}