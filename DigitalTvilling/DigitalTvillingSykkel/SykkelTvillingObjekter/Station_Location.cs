using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.SykkelTvillingObjekter
{
    /// <summary>Class that helps deconstructing the DTDL - model with corresponding values in the DTDL - model</summary>
    public class Station_Location
    {
        public double Lat { get; set; }

        public double Lon { get; set; }


        public Station_Location(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }


    }
}
