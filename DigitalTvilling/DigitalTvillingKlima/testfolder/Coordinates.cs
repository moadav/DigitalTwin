using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingKlima.testfolder
{
    public class Coordinates
    {
        public double Lat { get; set; }
        public double Lon { get; set; }

        public string Name { get; set; }

        public Coordinates(double lat, double lon, string name)
        {
            this.Lat = lat;
            this.Lon = lon;
            this.Name = name;
        }


    }
}
