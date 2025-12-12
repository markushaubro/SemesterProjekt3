using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gps;

namespace Gps
{
    public class GpsLibRepo
    {
        private readonly List<GpsLib> _gps = new();
        public GpsLibRepo GetById(int id)
        { 
            return _gps.Find(gps => gps.Id == id);
        }
        public GpsLibRepo Update(GpsLib New)
        {
           
        }
    }
}
