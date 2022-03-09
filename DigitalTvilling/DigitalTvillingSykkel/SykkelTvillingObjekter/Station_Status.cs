using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.SykkelTvillingObjekter
{
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
