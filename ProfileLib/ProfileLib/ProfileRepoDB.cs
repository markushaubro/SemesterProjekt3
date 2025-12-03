using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib
{
    public class ProfileRepoDB : IProfileList
    {
        private readonly ProfileRepoDBContext _context;
        public ProfileRepoDB(ProfileRepoDBContext dbContext)
        {
            _context = dbContext;
        }
        
        public IEnumerable<Profile> GetAll()
        {
            return _context.Profiles;
        }
        public Profile Add(Profile profile)
        {
            profile.ID = 0;
            _context.Profiles.Add(profile);
            _context.SaveChanges(); 
            return profile;
        }

        public Profile? GetById(int id)
        {
            return _context.Profiles.FirstOrDefault(p => p.ID == id);
        }

        public Profile? Remove(int id)
        {
            Profile? profile = GetById(id);
            if (profile is null)
            {
                return null;
            }
            _context.Profiles.Remove(profile);
            _context.SaveChanges();
            return profile;
        }

        public Profile? Update(int id, Profile Profile)
        {
            Profile? oldProfile = GetById(id);
            if (oldProfile is null)
            {
                return null;
            }
            oldProfile.Name = Profile.Name;
            oldProfile.Score = Profile.Score;
            _context.SaveChanges();
            return oldProfile;
        }


    }
}
