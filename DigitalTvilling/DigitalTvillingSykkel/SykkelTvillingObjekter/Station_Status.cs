using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.SykkelTvillingObjekter
{

    /// <summary> Primary Class that helps deconstructing the DTDL - model with corresponding values in the DTDL - model</summary>
    public class Station_Status
    {
        public Bicycle_Available Bicycle_Available { get; set; }

        public Station_Availablity Station_Availablity { get; set; }


        public Station_Status(Bicycle_Available bicycle_Available, Station_Availablity station_Availablity)
        {
            this.Bicycle_Available = bicycle_Available;
            this.Station_Availablity = station_Availablity;

        }



    }
}
