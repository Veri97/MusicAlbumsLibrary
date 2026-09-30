using MusicAlbumsLibrary.Application.Features.Libraries.DTOs;

namespace MusicAlbumsLibrary.Application.Abstractions.Features.Libraries;

public interface ILibraryService
{
    Task<List<CreatedAlbumDto>> AddAlbumsToLibraryAsync(int libraryId, List<AlbumDto> albums, CancellationToken cancellationToken);
    Task DeleteAlbumFromLibraryAsync(int albumId, CancellationToken cancellationToken);
}
