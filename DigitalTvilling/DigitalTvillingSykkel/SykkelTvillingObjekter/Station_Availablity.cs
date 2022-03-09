using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.SykkelTvillingObjekter
{

    public enum Station_Is_Installed
    {
        Not_Installed,
        Installed
    }
    public enum Station_Is_Renting
    {
        Not_Rented,
        Rented
    }
    public enum Station_Is_Returning 
    {
        Not_Returning,
        Returning
    }



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
