using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using LuftbornMusic.API.Controllers;
using LuftbornMusic.Core.Entities;
using LuftbornMusic.Core.Interfaces;

namespace LuftbornMusic.Tests;

public class PlaylistsControllerTests
{
    private readonly Mock<IPlaylistRepository> _mockRepo;
    private readonly PlaylistsController _controller;

    public PlaylistsControllerTests()
    {
        // 1. Setup the "Fake" Database Repository
        _mockRepo = new Mock<IPlaylistRepository>();
        
        // 2. Inject it into the Controller
        _controller = new PlaylistsController(_mockRepo.Object);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenPlaylistExists()
    {
        // Arrange
        var testGuid = Guid.NewGuid();
        var mockPlaylist = new Playlist { Id = testGuid, Name = "Test Vibes" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(testGuid)).ReturnsAsync(mockPlaylist);

        // Act
        var result = await _controller.GetById(testGuid);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Playlist>(okResult.Value);
        Assert.Equal("Test Vibes", returnValue.Name);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenPlaylistDoesNotExist()
    {
        // Arrange
        var testGuid = Guid.NewGuid();
        _mockRepo.Setup(repo => repo.GetByIdAsync(testGuid)).ReturnsAsync((Playlist)null!);

        // Act
        var result = await _controller.GetById(testGuid);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}