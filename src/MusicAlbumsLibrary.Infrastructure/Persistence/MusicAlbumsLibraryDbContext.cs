using Microsoft.EntityFrameworkCore;
using MusicAlbumsLibrary.Core.Entities;
using System.Reflection;

namespace MusicAlbumsLibrary.Infrastructure.Persistence;

public sealed class MusicAlbumsLibraryDbContext : DbContext
{
    public MusicAlbumsLibraryDbContext(DbContextOptions<MusicAlbumsLibraryDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Album> Albums { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}