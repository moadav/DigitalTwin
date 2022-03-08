
using DigitalTvillingSykkel.ApiDesc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


namespace DigitalTvillingSykkel.DigitalTwinRun
{
    public class DigitalTwinSykkelRun
    {
        public void RunSykkel()
        {
            ApiSykkel.InitalizeSykkelApi();
            apiResponse();



        }


        private async void apiResponse()
        {
            try
            {
                using (HttpResponseMessage response = await ApiSykkel.Client.GetAsync($"https://gbfs.urbansharing.com/oslobysykkel.no/station_information.json"))
                {


                    using (HttpResponseMessage response2 = await ApiSykkel.Client.GetAsync($"https://gbfs.urbansharing.com/oslobysykkel.no/station_status.json"))
                    {
                        if (!response.IsSuccessStatusCode && !response2.IsSuccessStatusCode)
                            Console.WriteLine("Response returned unsuccesfull");
                        else
                        {
                            Station_Info station_status_data = await response2.Content.ReadAsAsync<Station_Info>();

                            Console.WriteLine(station_status_data.Data.Stations[0].Is_Returning);

                            Station_Info station_info_data = await response.Content.ReadAsAsync<Station_Info>();

                            Console.WriteLine(station_info_data.Data.Stations[0].Address);
                        }



                    }

                }
            }catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }catch(ArgumentNullException a)
            {
                Console.WriteLine(a.Message);
            }
        }

    }
}
