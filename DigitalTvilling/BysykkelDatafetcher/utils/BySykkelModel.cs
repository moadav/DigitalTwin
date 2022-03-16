using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysykkelDatafetcher.utils
{
    public class StationInformation
    {

        public int last_updated { get; set; }
        public int ttl { get; set; }
        public string version { get; set; }
        public Data data { get; set; }
        public class Data
        {
            public List<Station> stations { get; set; }

            class RentalUris
            {
                string android { get; set; }
                string ios { get; set; }
            }
        }
    }

}
