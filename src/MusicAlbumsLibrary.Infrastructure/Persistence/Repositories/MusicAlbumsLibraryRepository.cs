using Microsoft.EntityFrameworkCore;
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

    public async Task AddLibraryAlbumsAsync(List<Album> albums, CancellationToken cancellationToken = default)
    {
        await _dbContext.Albums.AddRangeAsync(albums, cancellationToken);
    }

    public async Task<Library?> GetLibraryByIdAsync(int libraryId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Libraries.Include(x => x.Albums).FirstOrDefaultAsync(x => x.Id == libraryId, cancellationToken);
    }

    public async Task<Album?> GetAlbumByIdAsync(int albumId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == albumId, cancellationToken);
    }

    public void RemoveAlbum(Album album)
    {
        _dbContext.Albums.Remove(album);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}