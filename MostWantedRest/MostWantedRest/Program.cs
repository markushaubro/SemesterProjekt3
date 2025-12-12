using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MostWantedRest;
using ProfileLib;
using ProfileLib.RepoProfile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

bool useDatabase = true;
IProfileList _repo;

if (useDatabase)
{
    var optionsBuilder = new DbContextOptionsBuilder<ProfileRepoDBContext>();
    optionsBuilder.UseSqlServer(@"Data Source=mssql17.unoeuro.com;Initial Catalog=markusdokkedal_dk_db_recipeproject;User ID=markusdokkedal_dk;Password=5geaAcEB6nhkbmR23x4z;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    // connection string structure
    //   "Data Source=mssql7.unoeuro.com;Initial Catalog=FROM simply.com;Persist Security Info=True;User ID=FROM simply.com;Password=DB PASSWORD FROM simply.com;TrustServerCertificate=True"
    ProfileRepoDBContext _dbContext = new(optionsBuilder.Options);
    _repo = new ProfileRepoDB(_dbContext);
}
else
{
    _repo = new ProfileRepoList();
}
builder.Services.AddSingleton<IProfileList>(_repo);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
    
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
