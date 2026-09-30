using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicAlbumsLibrary.Core.Entities;

namespace MusicAlbumsLibrary.Infrastructure.Persistence.EntityConfigurations;

internal sealed class LibraryEntityConfiguration : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("Libraries");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.UserId)
               .IsRequired();

        builder.HasMany(l => l.Albums)
            .WithOne(l => l.Library)
            .HasForeignKey(l => l.LibraryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
