namespace Gps
{
    public class GpsLib
    {
        public int Id;

        private double _longitude;
        public double Longitude 
        {
            get { return _longitude; }
            set 
            { 
                if (value < 0)
                {
                    throw new ArgumentException("Cant' be null");
                }

                _longitude = value;
            }

            
        }
        private double _latitude;
        public double Latitude 
        { 
            get { return _latitude; }
            set 
            { 
                if (value < 0)
                {
                    throw new ArgumentException("Can't be null");
                }
            }
        }


        public GpsLib() { }

        public GpsLib(double longitude, double latitude)
        {
            Id = 1;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
