namespace LuftbornMusic.Core.Entities;

public class Song
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}