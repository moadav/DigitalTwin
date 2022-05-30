using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.ApiDesc
{
    /// <summary>Class used to dekonstruct the API</summary>
    public class Stations
    {
        public int Station_Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int Capacity { get; set; }
        public int Is_Installed { get; set; }
        public int Is_Renting { get; set; }
        public int Is_Returning { get; set; }
        public int Num_Bikes_Available { get; set; }
        public int Num_Docks_Available { get; set; }




    }
}
