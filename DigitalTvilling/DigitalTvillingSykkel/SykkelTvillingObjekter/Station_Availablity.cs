using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.SykkelTvillingObjekter
{

    /// <summary>checks if station is installed</summary>
    public enum Station_Is_Installed
    {
        Not_Installed,
        Installed
    }
    /// <summary>checks if station is renting</summary>
    public enum Station_Is_Renting
    {
        Not_Rented,
        Rented
    }
    /// <summary>checks if station is returning</summary>
    public enum Station_Is_Returning 
    {
        Not_Returning,
        Returning
    }


    /// <summary>Class that helps deconstructing the DTDL - model with corresponding values in the DTDL - model</summary>
    public class Station_Availablity
    {
        public Station_Is_Installed Station_Is_Installed { get; set; }
        public Station_Is_Renting Station_Is_Renting { get; set; }
        public Station_Is_Returning Station_Is_Returning { get; set; }

      

        public Station_Availablity(int is_installed, int is_Renting, int is_Returning)
        {

            Station_Is_Installed = (Station_Is_Installed) is_installed;
            Station_Is_Renting = (Station_Is_Renting) is_Renting;
            Station_Is_Returning = (Station_Is_Returning)is_Returning;

        }




    }
}
