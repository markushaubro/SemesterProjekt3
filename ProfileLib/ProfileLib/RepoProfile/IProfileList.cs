using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib.RepoProfile
{
    public interface IProfileList
    {
        Profile? Add(Profile profile);
        Profile? GetById(int id);
        Profile? Update(int id, Profile profile);
        Profile? Remove(int id);
        public IEnumerable<Profile> GetAll();

    }
}
