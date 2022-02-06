using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingKlima.testfolder
{
    public class Wind_info
    {
        public double Relative_humidity { get; set; }
        public double Wind_from_direction { get; set; }
        public double Wind_speed { get; set; }

        public Wind_info(double rh, double wfd, double ws)
        {
            Relative_humidity = rh;
            Wind_from_direction = wfd;
            Wind_speed = ws;
        }

    }
}
