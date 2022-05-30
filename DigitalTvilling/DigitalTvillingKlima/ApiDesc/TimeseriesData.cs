using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima
{
    /// <summary>Class used to dekonstruct the API</summary>
    public class TimeseriesData
    {

        public TimeseriesInstant Instant { get; set; }
        public WeatherNext1Hour Next_1_hours { get; set; }
        



    }
}
