using LuftbornMusic.Core.Entities;
using LuftbornMusic.Core.Interfaces;
using LuftbornMusic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LuftbornMusic.Infrastructure.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly AppDbContext _context;

    public PlaylistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Playlist?> GetByIdAsync(Guid id)
    {
        return await _context.Playlists
            .Include(p => p.Songs) 
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Playlist>> GetAllAsync()
    {
        return await _context.Playlists
            .Include(p => p.Songs)
            .ToListAsync(); 
    }

    public async Task AddAsync(Playlist playlist)
    {
        await _context.Playlists.AddAsync(playlist);
        await _context.SaveChangesAsync(); 
    }

    public async Task UpdateAsync(Playlist playlist)
    {
        _context.Playlists.Update(playlist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var playlist = await _context.Playlists.FindAsync(id);
        if (playlist != null)
        {
            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
        }
    }
}