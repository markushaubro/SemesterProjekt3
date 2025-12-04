// Program.cs
using Microsoft.EntityFrameworkCore;
using fbiController;

var builder = WebApplication.CreateBuilder(args);

// --- EF Core / DB ---
builder.Services.AddDbContext<WantedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WantedDb")));

// --- HttpClient for FBI API (named "fbi") ---
builder.Services.AddHttpClient("fbi", client =>
{
    client.BaseAddress = new Uri("https://api.fbi.gov/");
    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; FbiCacheApp/1.0)");
});

// --- Controllers + CORS ---
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();
app.MapControllers();

app.Run();
