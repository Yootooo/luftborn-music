using LuftbornMusic.Core.Entities;

namespace LuftbornMusic.Core.Interfaces;

public interface IPlaylistRepository
{
    Task<Playlist?> GetByIdAsync(Guid id);
    Task<IEnumerable<Playlist>> GetAllAsync();
    Task AddAsync(Playlist playlist);
    Task UpdateAsync(Playlist playlist);
    Task DeleteAsync(Guid id);
}