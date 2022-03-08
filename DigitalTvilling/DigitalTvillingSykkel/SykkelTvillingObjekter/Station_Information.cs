using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.SykkelTvillingObjekter
{
    public class Station_Information
    {
        public int Station_Id { get; set; }

        public string Station_Name { get; set; }

        public string Station_Address { get; set; }

        public int Station_Capacity { get; set; }

        public Station_Location Station_Location {get; set; }


        public Station_Information(int station_Id, string station_Name, string station_Address, int station_Capacity, Station_Location station_Location)
        {
            this.Station_Id = station_Id;
            this.Station_Name = station_Name;
            this.Station_Address = station_Address;
            this.Station_Capacity = station_Capacity;
            this.Station_Location = station_Location;

        }



    }
}
