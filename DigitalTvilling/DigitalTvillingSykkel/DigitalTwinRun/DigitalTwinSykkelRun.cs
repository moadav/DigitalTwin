
using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Hjelpeklasser;
using DigitalTvillingSykkel.ApiDesc;
using DigitalTvillingSykkel.DigitalTwin;
using DigitalTvillingSykkel.SykkelTvillingObjekter;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


namespace DigitalTvillingSykkel.DigitalTwinRun
{
    public class DigitalTwinSykkelRun
    {
        DigitalTwinsClient Client = DigitalTwinsInstansiateClient.DigitalTwinsClient(new Uri("https://dthiofadt.api.weu.digitaltwins.azure.net"));


        public void RunSykkel()
        {
            ApiSykkel.InitalizeSykkelApi();
            //ApiResponseAsync();
            CreateNeededTwinAndRelationshipAsync();


        }


        private async void ApiResponseAsync()
        {
            try
            {
                using (HttpResponseMessage response = await ApiSykkel.Client.GetAsync($"https://gbfs.urbansharing.com/oslobysykkel.no/station_information.json"))
                {


                    using (HttpResponseMessage response2 = await ApiSykkel.Client.GetAsync($"https://gbfs.urbansharing.com/oslobysykkel.no/station_status.json"))
                    {

                        ReadResponseAsync(response,response2);


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

        private async void ReadResponseAsync(HttpResponseMessage response, HttpResponseMessage response2)
        {
            if (!response.IsSuccessStatusCode && !response2.IsSuccessStatusCode)
                Console.WriteLine("Response returned unsuccesfull");
            else
            {

                Station_Info station_status_data = await response2.Content.ReadAsAsync<Station_Info>();

                Station_Info station_info_data = await response.Content.ReadAsAsync<Station_Info>();
                ReadValues(station_status_data, station_info_data);

              

            }
        }

        private void ReadValues(Station_Info station_status_data, Station_Info station_info_data)
        {

            Console.WriteLine(station_status_data.Data.Stations[0].Is_Returning);
            Console.WriteLine(station_info_data.Data.Stations[0].Address);



            Station_Information a = new Station_Information();
            Station_Status b = new Station_Status();
            //CreateSykkelTwinsAsync(a, "dtmi:oslo:sykkler:sykkel;1",b);

        }

        private async void CreateNeededTwinAndRelationshipAsync()
        {
            string idNavn = "Oslo_Sykler";
            string targetId = "Oslo";
            string relationName = "Oslo_har_sykler";

            var twinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = "dtmi:oslo:oslo_sykler;1"
                },
              
                Id = idNavn
            };
            Client.CreateOrReplaceDigitalTwin(twinContents.Id, twinContents);

           await Relationshipbuilder.CreateRelationshipAsync(Client,twinContents.Id,targetId,relationName );

        }

        private async void CreateSykkelTwinsAsync(Station_Information station_Information, string idNavn, Station_Status station_Status)
        {
            SykkelTwin twins = new SykkelTwin();

            BasicDigitalTwin contents = twins.CreateSykkelTwinContents(station_Information, idNavn, station_Status);

            twins.CreateTwinsAsync(Client, contents);

            await Relationshipbuilder.CreateRelationshipAsync(Client, contents.Id, "Oslo", "Oslo_har_bydel");

        }

    }
}
