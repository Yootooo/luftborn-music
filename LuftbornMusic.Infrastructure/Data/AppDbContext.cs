using LuftbornMusic.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuftbornMusic.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Song> Songs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<Playlist>()
            .HasMany(p => p.Songs)
            .WithMany(s => s.Playlists);
    }
}