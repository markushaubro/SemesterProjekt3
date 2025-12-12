using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib.RepoProfile
{
    public class ProfileRepoList : IProfileList
    {
        private int _nextid;
        private readonly List<Profile> _profiles = new();

        public ProfileRepoList() 
        {
            _profiles.Add(new Profile { ID = _nextid++, Name = "Alice", Score = 85 });
            _profiles.Add(new Profile { ID = _nextid++, Name = "Bob", Score = 90 });
            _profiles.Add(new Profile { ID = _nextid++, Name = "Charlie", Score = 78 });
            _profiles.Add(new Profile { ID = _nextid++, Name = "Diana", Score = 92 });
            _profiles.Add(new Profile { ID = _nextid++, Name = "Eve", Score = 88 });
        }

        public List<Profile> GetAll1()
        {
            return new List<Profile>(_profiles);
        }
        public Profile Add(Profile profile)
        {
            profile.ID = _nextid++;
            _profiles.Add(profile);
            return profile;
        }

        public Profile? GetById(int id)
        {
            return _profiles.Find(profile => profile.ID == id);
        }

        public Profile? Remove(int id)
        {
            Profile? findProfile = GetById(id);
            if (findProfile == null)
            {
                throw new Exception("Profile can't be found");
            }
            _profiles.Remove(findProfile);
            return findProfile;
        }

        public Profile? Update(int id, Profile newProfile)
        {
            Profile? oldProfile = GetById(id);
            if (oldProfile == null)
            {
                throw new ArgumentNullException("No Profile Found");
            }
            oldProfile.Name = newProfile.Name;
            oldProfile.Score = newProfile.Score;
            return oldProfile; 
        }

        public IEnumerable<Profile> GetAll()
        { 
            return _profiles; 
        }
    }
}
