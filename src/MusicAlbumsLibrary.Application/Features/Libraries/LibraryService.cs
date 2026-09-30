using MusicAlbumsLibrary.Application.Abstractions.Features.Libraries;
using MusicAlbumsLibrary.Application.Exceptions;
using MusicAlbumsLibrary.Application.Features.Libraries.DTOs;
using MusicAlbumsLibrary.Core.Contracts;
using MusicAlbumsLibrary.Core.Entities;

namespace MusicAlbumsLibrary.Application.Features.Libraries;

public class LibraryService : ILibraryService
{
    private readonly IMusicAlbumsLibraryRepository _musicAlbumsLibraryRepository;

    public LibraryService(IMusicAlbumsLibraryRepository musicAlbumsLibraryRepository)
    {
        _musicAlbumsLibraryRepository = musicAlbumsLibraryRepository;
    }

    public async Task<List<CreatedAlbumDto>> AddAlbumsToLibraryAsync(int libraryId, List<AlbumDto> albums, CancellationToken cancellationToken)
    {
        var library = await _musicAlbumsLibraryRepository.GetLibraryByIdAsync(libraryId, cancellationToken);

        if (library is null)
        {
            throw new NotFoundException($"Library with Id {libraryId} not found.");
        }

        var existingLibraryAlbumsIdKeys = library.Albums
            .Select(x => (x.MusicCatalogueProvider, x.MusicCatalogueProviderAlbumId)).ToHashSet();

        var albumsToAdd = albums
            .Where(x => !existingLibraryAlbumsIdKeys.Any(y =>
              string.Equals(y.MusicCatalogueProvider, x.MusicCatalogueProvider, StringComparison.OrdinalIgnoreCase) &&
              string.Equals(y.MusicCatalogueProviderAlbumId, x.MusicCatalogueProviderAlbumId, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        var albumEntities = albumsToAdd.Select(x => new Album
        {
            LibraryId = libraryId,
            MusicCatalogueProvider = x.MusicCatalogueProvider,
            MusicCatalogueProviderAlbumId = x.MusicCatalogueProviderAlbumId,
            ArtistName = x.ArtistName,
            AlbumName = x.AlbumName,
            CoverUrl = x.CoverUrl,
            AlbumUrl = x.AlbumUrl
        }).ToList();

        await _musicAlbumsLibraryRepository.AddLibraryAlbumsAsync(albumEntities, cancellationToken);
        await _musicAlbumsLibraryRepository.SaveChangesAsync(cancellationToken);

        return albumEntities.Select(x => new CreatedAlbumDto
        {
            Id = x.Id,
            LibraryId = x.LibraryId,
            MusicCatalogueProvider = x.MusicCatalogueProvider,
            MusicCatalogueProviderAlbumId = x.MusicCatalogueProviderAlbumId,
            ArtistName = x.ArtistName,
            AlbumName = x.AlbumName,
            CoverUrl = x.CoverUrl,
            AlbumUrl = x.AlbumUrl
        }).ToList();
    }

    public async Task DeleteAlbumFromLibraryAsync(int albumId, CancellationToken cancellationToken)
    {
        var album = await _musicAlbumsLibraryRepository.GetAlbumByIdAsync(albumId, cancellationToken);

        if (album is null)
        {
            throw new NotFoundException($"Album with Id {albumId} not found.");
        }

        _musicAlbumsLibraryRepository.RemoveAlbum(album);
        await _musicAlbumsLibraryRepository.SaveChangesAsync(cancellationToken);
    }
}
