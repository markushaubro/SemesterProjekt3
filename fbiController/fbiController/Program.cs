using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using fbiController;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WantedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WantedDb")));

builder.Services.AddHttpClient("fbi", client =>
{
    client.BaseAddress = new Uri("https://api.fbi.gov/");
    client.DefaultRequestHeaders.UserAgent.Clear();
    client.DefaultRequestHeaders.UserAgent.Add(
        new ProductInfoHeaderValue("ZealandFbiSync", "1.0"));
    client.DefaultRequestHeaders.UserAgent.Add(
        new ProductInfoHeaderValue("(contact:student@example.com)"));
});

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FBI Wanted API",
        Version = "v1",
        Description = "Internal API that stores FBI Most Wanted data, synced from the public feed."
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FBI API v1");
});

app.MapControllers();

app.Run();
