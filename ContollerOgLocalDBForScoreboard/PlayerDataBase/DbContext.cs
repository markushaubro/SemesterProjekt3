using Microsoft.EntityFrameworkCore;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options) { }

    public DbSet<PlayerScoreboard> PlayerScoreboards { get; set; }
}
