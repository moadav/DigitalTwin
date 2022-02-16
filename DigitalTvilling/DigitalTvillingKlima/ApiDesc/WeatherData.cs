using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima
{
   public class WeatherData
    {
        public string Updated_at { get; set; }
        public WeatherMeta Meta { get; set; }

        public List<WeatherTimeseries> TimeSeries { get; set; }



    }
}
