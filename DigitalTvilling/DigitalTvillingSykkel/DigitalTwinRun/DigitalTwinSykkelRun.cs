
using DigitalTvillingSykkel.ApiDesc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


namespace DigitalTvillingSykkel.DigitalTwinRun
{
    public class DigitalTwinSykkelRun
    {
        public async void RunSykkel()
        {
            ApiSykkel.InitalizeSykkelApi();
            
            using (HttpResponseMessage response = await ApiSykkel.Client.GetAsync($"https://gbfs.urbansharing.com/oslobysykkel.no/station_information.json"))
            {
                Station_Info data = await response.Content.ReadAsAsync<Station_Info>();

                Console.WriteLine(data.Data.Stations[0].Address);
              
            }


            using (HttpResponseMessage response2 = await ApiSykkel.Client.GetAsync($"https://gbfs.urbansharing.com/oslobysykkel.no/station_status.json"))
            {
                Station_Info data = await response2.Content.ReadAsAsync<Station_Info>();

                Console.WriteLine(data.Data.Stations[0].Is_Returning);
              
            }



        }
        


    }
}
