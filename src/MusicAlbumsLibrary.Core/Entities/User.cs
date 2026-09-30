namespace MusicAlbumsLibrary.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Library Library { get; set; } = null!;
}