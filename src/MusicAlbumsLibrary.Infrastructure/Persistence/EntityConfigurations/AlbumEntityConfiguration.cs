using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicAlbumsLibrary.Core.Entities;

namespace MusicAlbumsLibrary.Infrastructure.Persistence.EntityConfigurations;

internal sealed class AlbumEntityConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("Albums");
        builder.HasKey(a => a.Id);

        builder.HasIndex(a => new { a.LibraryId, a.MusicCatalogueProvider, a.MusicCatalogueProviderAlbumId })
               .IsUnique();

        builder.Property(a => a.MusicCatalogueProvider)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(a => a.MusicCatalogueProviderAlbumId)
               .IsRequired();

        builder.Property(a => a.ArtistName)
               .HasMaxLength(500)
               .IsRequired();

        builder.Property(a => a.AlbumName)
               .HasMaxLength(500)
               .IsRequired();

        builder.Property(a => a.CoverUrl)
               .HasMaxLength(2500);

        builder.Property(a => a.AlbumUrl)
               .HasMaxLength(2500)
               .IsRequired();
    }
}