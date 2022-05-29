using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingKlima.testfolder
{
    /// <summary>Class that helps deconstructing the DTDL - model with corresponding values in the DTDL - model</summary>
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
