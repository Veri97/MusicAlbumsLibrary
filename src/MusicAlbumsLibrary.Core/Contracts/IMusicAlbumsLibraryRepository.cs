using MusicAlbumsLibrary.Core.Entities;

namespace MusicAlbumsLibrary.Core.Contracts;

public interface IMusicAlbumsLibraryRepository
{
    Task CreateUserAsync(User user, CancellationToken cancellationToken = default);
    Task AddLibraryAlbumsAsync(List<Album> albums, CancellationToken cancellationToken = default);
    Task<Library?> GetLibraryByIdAsync(int libraryId, CancellationToken cancellationToken = default);
    Task<Album?> GetAlbumByIdAsync(int albumId, CancellationToken cancellationToken = default);
    void RemoveAlbum(Album album);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
