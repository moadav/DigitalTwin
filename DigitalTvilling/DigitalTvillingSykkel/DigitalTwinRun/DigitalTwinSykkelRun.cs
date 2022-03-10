
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

        private int Station_Id { get; set; }
        private string Station_Name { get; set; }
        private string Station_Address { get; set; }
        private int Station_Capacity { get; set; }
        private int Num_bikes_Available { get; set; }
        private int Num_docks_Available { get; set; }
        private int Station_is_installed { get; set; }
        private int Station_is_renting { get; set; }
        private int Station_is_returning { get; set; }
        private double Lon { get; set; }
        private double Lat { get; set; }




        public void RunSykkel()
        {
            ApiSykkel.InitalizeSykkelApi();
            ApiResponseAsync();
            //CreateNeededTwinAndRelationshipAsync();


        }


        private async void ApiResponseAsync()
        {
            try
            {
                using (HttpResponseMessage response = await ApiSykkel.Client.GetAsync($"https://gbfs.urbansharing.com/oslobysykkel.no/station_information.json"))
                {


                    using (HttpResponseMessage response2 = await ApiSykkel.Client.GetAsync($"https://gbfs.urbansharing.com/oslobysykkel.no/station_status.json"))
                    {

                        ReadResponseAsync(response, response2);


                    }

                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentNullException a)
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

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < station_info_data.Data.Stations.Count; j++)
                {
                    if (station_status_data.Data.Stations[i].Station_Id == station_info_data.Data.Stations[j].Station_Id)
                    {
                        FixValues(station_info_data.Data.Stations[j], station_status_data.Data.Stations[i]);

                        CreateTwin();
                    }
                }
            }
        }
        private void CreateTwin()
        {
            Station_Location loca = new Station_Location(Lat, Lon);
            Station_Information station_info = new Station_Information(Station_Id, Station_Name, Station_Address, Station_Capacity, loca);

            Station_Status station_status = new Station_Status(new Bicycle_Available(Num_bikes_Available, Num_docks_Available),
                new Station_Availablity(Station_is_installed, Station_is_renting, Station_is_returning));



            CreateSykkelTwinsAsync(station_info, $"station_{Station_Id}", station_status);
        }
        private void FixValues(Stations station_Information, Stations station_Status)
        {


            Station_Id = station_Information.Station_Id;
            Station_Name = station_Information.Name;
            Station_Address = station_Information.Address;
            Station_Capacity = station_Information.Capacity;
            Num_bikes_Available = station_Status.Num_Bikes_Available;
            Num_docks_Available = station_Status.Num_Docks_Available;
            Station_is_installed = station_Status.Is_Installed;
            Station_is_renting = station_Status.Is_Renting;
            Station_is_returning = station_Status.Is_Returning;
            Lon = station_Information.Lon;
            Lat = station_Information.Lat;
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

            await Relationshipbuilder.CreateRelationshipAsync(Client, twinContents.Id, targetId, relationName);

        }

        private async void CreateSykkelTwinsAsync(Station_Information station_Information, string idNavn, Station_Status station_Status)
        {
            SykkelTwin twins = new SykkelTwin();

            BasicDigitalTwin contents = twins.CreateSykkelTwinContents(station_Information, idNavn, station_Status);

            twins.CreateTwinsAsync(Client, contents);

            await Relationshipbuilder.CreateRelationshipAsync(Client, contents.Id, "Oslo_Sykler", "sykkler_har_sykkel");

        }

    }
}
