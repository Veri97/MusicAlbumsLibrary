using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicAlbumsLibrary.Core.Entities;

namespace MusicAlbumsLibrary.Infrastructure.Persistence.EntityConfigurations;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
               .HasMaxLength(250)
               .IsRequired();

        builder.HasOne(u => u.Library)
               .WithOne(u => u.User)
               .HasForeignKey<Library>(u => u.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
