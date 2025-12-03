using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib
{
    public class ProfileRepoDBContext : DbContext
    {
        public ProfileRepoDBContext(
            DbContextOptions<ProfileRepoDBContext> options) :
            base(options)
        { }

        public DbSet<Profile> Profiles { get; set; }
    }
       
}
