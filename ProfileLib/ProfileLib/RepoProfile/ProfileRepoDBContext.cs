using Microsoft.EntityFrameworkCore;
using ProfileLib.RepoGameService;
using ProfileLib.RepoVillains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib.RepoProfile
{
    public class ProfileRepoDBContext : DbContext
    {
        public ProfileRepoDBContext(
            DbContextOptions<ProfileRepoDBContext> options) :
            base(options)
        { }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<CurrentUser> CurrentUsers { get; set; }
        public DbSet<Villain> Villains { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CurrentUser>()
                .HasOne(cu => cu.Profile)
                .WithMany()
                .HasForeignKey(cu => cu.ProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CurrentUser>()
                .HasIndex(cu => cu.Id)
                .IsUnique();

            modelBuilder.Entity<Villain>()
                .HasOne(v => v.CaughtByUser)
                .WithMany()
                .HasForeignKey(v => v.CaughtByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Villain>()
                .Property(v => v.IsActive)
                .HasDefaultValue(true);
        }
    }
       
}
