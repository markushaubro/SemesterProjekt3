using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib
{
    public class VillainRepo : IVillianRepo
    {
        private readonly ProfileRepoDBContext _context;

        public VillainRepo(ProfileRepoDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Villain> GetAll()
        {
            return _context.Villains.ToList();
        }

        public Villain? GetById(int id)
        {
            return _context.Villains.FirstOrDefault(v => v.Id == id);
        }

        public Villain Add(Villain villain)
        {
            villain.Id = 0;
            _context.Villains.Add(villain);
            _context.SaveChanges();
            return villain;
        }

        public void DeleteAll()
        {
            _context.Villains.RemoveRange(_context.Villains);
            _context.SaveChanges();
        }

        public IEnumerable<Villain> GetActiveVillains()
        {
            return _context.Villains.Where(v => v.IsActive).ToList();
        }
    }
}
