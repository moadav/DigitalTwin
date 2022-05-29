using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima
{
    /// <summary>
    ///   class to deconstruct the api 
    /// </summary>
    public class WeatherUnits
    {

        public string Air_pressure_at_sea_level { get; set; }
        public string Air_temperature { get; set; }
        public string Cloud_area_fraction { get; set; }
        public string Precipitation_amount { get; set; }
        public string Relative_humidity { get; set; }
        public string Wind_from_direction { get; set; }
        public string Wind_speed { get; set; }
    }
}
