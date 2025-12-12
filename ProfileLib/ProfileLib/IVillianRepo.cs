using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib
{
    public interface IVillianRepo
    {
        IEnumerable<Villain> GetAll();
        Villain? GetById(int id);
        Villain Add(Villain villain);
        void DeleteAll();
        IEnumerable<Villain> GetActiveVillains();
    }
}
