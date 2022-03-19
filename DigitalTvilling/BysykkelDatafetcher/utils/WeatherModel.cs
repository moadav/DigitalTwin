using System;
using System.Collections.Generic;
using System.Text;

namespace BysykkelDatafetcher.utils
{
    public class WeatherForecast
    {
        private string type { get; set; }
        private Geometry geometry { get; set; }
        public Properties properties { get; set; }

        private class Geometry
        {
            private string type { get; set; }
            private List<double> coordinates { get; set; }
        }
        public class Properties
        {
            private Meta meta { get; set; }
            public List<Timesery> timeseries { get; set; }

            private class Meta
            {
                private DateTime updated_at { get; set; }
                private Units units { get; set; }

                private class Units
                {
                    private string air_pressure_at_sea_level { get; set; }
                    private string air_temperature { get; set; }
                    private string cloud_area_fraction { get; set; }
                    private string precipitation_amount { get; set; }
                    private string relative_humidity { get; set; }
                    private string wind_from_direction { get; set; }
                    private string wind_speed { get; set; }
                }
            }
            public class Timesery
            {
                public DateTime time { get; set; }
                public Data data { get; set; }

                public class Data
                {
                    public Instant instant { get; set; }
                    private Next12Hours next_12_hours { get; set; }
                    public Next1Hours next_1_hours { get; set; }
                    private Next6Hours next_6_hours { get; set; }

                    public class Instant
                    {
                        public Details details { get; set; }
                    }
                    private class Next12Hours
                    {
                        private Summary summary { get; set; }
                    }

                    public class Next1Hours
                    {
                        private Summary summary { get; set; }
                        public Details details { get; set; }
                    }

                    private class Next6Hours
                    {
                        private Summary summary { get; set; }
                        private Details details { get; set; }
                    }
                    public class Details
                    {
                        public double air_pressure_at_sea_level { get; set; }
                        public double air_temperature { get; set; }
                        public double cloud_area_fraction { get; set; }
                        public double relative_humidity { get; set; }
                        public double wind_from_direction { get; set; }
                        public double wind_speed { get; set; }
                        public double precipitation_amount { get; set; }
                    }
                    private class Summary
                    {
                        private string symbol_code { get; set; }
                    }
                }
            }
        }
    }

}
