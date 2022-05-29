using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima
{

    public class Feature
    {
        public string Id { get; set; }
        public GeoData Geometry { get; set; }

        public WeatherData Properties { get; set; }


    }
}
