using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingKlima.testfolder
{

    /// <summary> Primary Class that helps deconstructing the DTDL - model with corresponding values in the DTDL - model</summary>
    public class KlimaInfo
    {

        public string Symbole_code { get; set; }
        public string Time { get; set; }
        public Air_info Air_info { get; set; }
        public Wind_info Wind_info { get; set; }

        public KlimaInfo(string sc, string t, Air_info air_Info, Wind_info wind_Info)
        {
            Symbole_code = sc;
            Time = t;
            Air_info = air_Info;
            Wind_info = wind_Info;
           
            
        }

       

    }
}
