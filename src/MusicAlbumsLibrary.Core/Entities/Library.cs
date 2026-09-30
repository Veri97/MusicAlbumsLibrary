namespace MusicAlbumsLibrary.Core.Entities;

public class Library
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<Album> Albums { get; set; } = null!;
}
