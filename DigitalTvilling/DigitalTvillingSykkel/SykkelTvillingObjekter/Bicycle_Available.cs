using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.SykkelTvillingObjekter
{
    /// <summary>Class that helps deconstructing the DTDL - model with corresponding values in the DTDL - model</summary>
    public class Bicycle_Available
    {
        public int Num_Bikes_Available { get; set; }

        public int Num_Docks_Available { get; set; }

        public Bicycle_Available(int num_Bikes_Available, int num_Docks_Available)
        {
            this.Num_Bikes_Available = num_Bikes_Available;
            this.Num_Docks_Available = num_Docks_Available;
        }


    }
}
