using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib.RepoProfile
{
    public class ProfileRepoDBContextFactory : IDesignTimeDbContextFactory<ProfileRepoDBContext>
    {
        public ProfileRepoDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProfileRepoDBContext>();

            // Connection string til design-time (migrations)
            optionsBuilder.UseSqlServer(@"Data Source=mssql17.unoeuro.com;Initial Catalog=markusdokkedal_dk_db_recipeproject;User ID=markusdokkedal_dk;Password=5geaAcEB6nhkbmR23x4z;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            return new ProfileRepoDBContext(optionsBuilder.Options);
        }
    }
}
