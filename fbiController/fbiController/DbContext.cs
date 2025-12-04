// DbContext.cs
using Microsoft.EntityFrameworkCore;
using fbiController.Models;

namespace fbiController
{
    public class WantedDbContext : DbContext
    {
        public WantedDbContext(DbContextOptions<WantedDbContext> options)
            : base(options)
        {
        }

        public DbSet<WantedPerson> WantedPeople { get; set; } = null!;
    }
}
