using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingKlima.testfolder
{
    public class Coordinates
    {
        public double Lat { get; set; }
        public double Lon { get; set; }

        public string StedNavn { get; set; }

        public Coordinates(double lat, double lon, string sn)
        {
            Lat = lat;
            Lon = lon;
            StedNavn = sn;
        }


    }
}
