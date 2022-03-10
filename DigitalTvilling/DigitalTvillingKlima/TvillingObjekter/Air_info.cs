﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingKlima.testfolder
{
    
    public class Air_info
    {
        
        public double Air_pressure_at_sea_level { get; set; }
        public double Air_temperature { get; set; }
        public double Cloud_area_fraction { get; set; }

        public Air_info(double apasl, double at, double caf)
        {
            Air_pressure_at_sea_level = apasl;
            Air_temperature = at;
            Cloud_area_fraction = caf;
        }


    }
}
