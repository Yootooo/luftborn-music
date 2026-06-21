using Microsoft.EntityFrameworkCore;
using LuftbornMusic.Core.Interfaces;
using LuftbornMusic.Infrastructure.Data;
using LuftbornMusic.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- TEMPORARY IN-MEMORY SETUP ---
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseInMemoryDatabase("LuftbornMusicTestDb"));

// the next 2 lines will be commented for in memory database 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // This gives us a beautiful UI to test our API

var app = builder.Build();

// and those liens too 
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); 
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();