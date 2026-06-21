using Microsoft.AspNetCore.Mvc;
using LuftbornMusic.Core.Entities;
using LuftbornMusic.Core.Interfaces;

namespace LuftbornMusic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistRepository _repository;

    // Dependency Injection hands us the Repository we built earlier!
    public PlaylistsController(IPlaylistRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var playlists = await _repository.GetAllAsync();
        return Ok(playlists);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var playlist = await _repository.GetByIdAsync(id);
        if (playlist == null) return NotFound();
        return Ok(playlist);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Playlist playlist)
    {
        // A real production app would use DTOs here, but this is perfect for the test scope
        playlist.Id = Guid.NewGuid();
        await _repository.AddAsync(playlist);
        return CreatedAtAction(nameof(GetById), new { id = playlist.Id }, playlist);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Playlist playlist)
    {
        if (id != playlist.Id) return BadRequest();
        await _repository.UpdateAsync(playlist);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}