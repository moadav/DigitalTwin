
using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Hjelpeklasser;
using DigitalTvillingSykkel.ApiDesc;
using DigitalTvillingSykkel.DigitalTwin;
using DigitalTvillingSykkel.SykkelTvillingObjekter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


namespace DigitalTvillingSykkel.DigitalTwinRun
{
    public class DigitalTwinSykkelRun
    {
        /// <summary>The DigitalTwinsClient which connects to the Azure Digital Twin plattform</summary>
        DigitalTwinsClient Client = DigitalTwinsInstansiateClient.DigitalTwinsClient();


        /// <summary>Gets or sets the station identifier.</summary>
        /// <value>The station identifier.</value>
        private int Station_Id { get; set; }

        /// <summary>Gets or sets the name of the station.</summary>
        /// <value>The name of the station.</value>
        private string Station_Name { get; set; }


        /// <summary>Gets or sets the station address.</summary>
        /// <value>The station address.</value>
        private string Station_Address { get; set; }


        /// <summary>Gets or sets the station capacity.</summary>
        /// <value>The station capacity.</value>
        private int Station_Capacity { get; set; }


        /// <summary>Gets or sets the number bikes available.</summary>
        /// <value>The number bikes available.</value>
        private int Num_bikes_Available { get; set; }


        /// <summary>Gets or sets the number docks available.</summary>
        /// <value>The number docks available.</value>
        private int Num_docks_Available { get; set; }


        /// <summary>Gets or sets the station is installed.</summary>
        /// <value>The station is installed.</value>
        private int Station_is_installed { get; set; }


        /// <summary>Gets or sets the station is renting.</summary>
        /// <value>The station is renting.</value>
        private int Station_is_renting { get; set; }


        /// <summary>Gets or sets the station is returning.</summary>
        /// <value>The station is returning.</value>
        private int Station_is_returning { get; set; }


        /// <summary>Gets or sets the lon.</summary>
        /// <value>The lon.</value>
        private double Lon { get; set; }


        /// <summary>Gets or sets the lat.</summary>
        /// <value>The lat.</value>
        private double Lat { get; set; }




        /// <summary>Method that initializes the API and gets response from api and creates twins</summary>
        public void RunSykkel()
        {
            ApiSykkel.InitalizeSykkelApi();
            //CreateNeededTwinAndRelationshipAsync();
            ApiResponseAsync();
            
        }


        /// <summary>Handles the repsonse from API asynchronous.</summary>
        private async void ApiResponseAsync()
        {
            try
            {
                using (HttpResponseMessage Response = await ApiSykkel.Client.GetAsync($"https://gbfs.urbansharing.com/oslobysykkel.no/station_information.json"))
                {

                    using (HttpResponseMessage Response2 = await ApiSykkel.Client.GetAsync($"https://gbfs.urbansharing.com/oslobysykkel.no/station_status.json"))
                    {
                        Response.EnsureSuccessStatusCode();
                        Response2.EnsureSuccessStatusCode();
                        ReadResponseAsync(Response, Response2);
                    }

                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }
            catch (HttpRequestException k)
            {
                Console.WriteLine(k);
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Reads the response asynchronous and deconstructs from JSON to datatype values.</summary>
        /// <param name="Response">The response from station_information api <see cref="HttpResponseMessage"/></param>
        /// <param name="Response2">The response2 from station_status api <see cref="HttpResponseMessage"/></param>
        private async void ReadResponseAsync(HttpResponseMessage Response, HttpResponseMessage Response2)
        {
            Station_Info station_status_data = await Response2.Content.ReadAsAsync<Station_Info>();

            Station_Info station_info_data = await Response.Content.ReadAsAsync<Station_Info>();
            ReadValues(station_status_data, station_info_data);
        }

        /// <summary>Reads the values from API</summary>
        /// <param name="Station_Status_Data">The station status data. <see cref="Station_Info"/></param>
        /// <param name="Station_Info_Data">The station information data. <see cref="Station_Info"/></param>
        private void ReadValues(Station_Info Station_Status_Data, Station_Info Station_Info_Data)
        {

            for (int i = 0; i < Station_Status_Data.Data.Stations.Count; i++)
            {
                for (int j = 0; j < Station_Info_Data.Data.Stations.Count; j++)
                {
                    if (Station_Status_Data.Data.Stations[i].Station_Id == Station_Info_Data.Data.Stations[j].Station_Id)
                    {
                        FixValues(Station_Info_Data.Data.Stations[j], Station_Status_Data.Data.Stations[i]);

                        CreateTwin();
                    }
                }
            }
        }


        /// <summary>Creates the twin.</summary>
        private void CreateTwin()
        {
            Station_Location loca = new Station_Location(Lat, Lon);
            Station_Information Station_Info = new Station_Information(Station_Id, Station_Name, Station_Address, Station_Capacity, loca);

            Station_Status Station_Status = new Station_Status(new Bicycle_Available(Num_bikes_Available, Num_docks_Available),
                new Station_Availablity(Station_is_installed, Station_is_renting, Station_is_returning));

            CreateSykkelTwinAsync(Station_Info, $"station_{Station_Id}", Station_Status);
        }


        /// <summary>Takes values from API's and sets values specified in class.</summary>
        /// <param name="Station_Information">value with the API from The station information <see cref="Stations"/></param>
        /// <param name="Station_Status">value with the API fromThe station status.<see cref="Stations"/></param>
        private void FixValues(Stations Station_Information, Stations Station_Status)
        {


            Station_Id = Station_Information.Station_Id;
            Station_Name = Station_Information.Name;
            Station_Address = Station_Information.Address;
            Station_Capacity = Station_Information.Capacity;
            Num_bikes_Available = Station_Status.Num_Bikes_Available;
            Num_docks_Available = Station_Status.Num_Docks_Available;
            Station_is_installed = Station_Status.Is_Installed;
            Station_is_renting = Station_Status.Is_Renting;
            Station_is_returning = Station_Status.Is_Returning;
            Lon = Station_Information.Lon;
            Lat = Station_Information.Lat;
        }


        /// <summary>Creates a static twin (a twin to build relationship with the other bicycle twins to have a prettier graph) and relationship asynchronous.</summary>
        private async void CreateNeededTwinAndRelationshipAsync()
        {
            string IdNavn = "Oslo_Sykler";
            string TargetId = "Oslo";
            string RelationName = "Oslo_har_sykler";

            var TwinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = "dtmi:oslo:oslo_sykler;1"
                },

                Id = IdNavn
            };
            Client.CreateOrReplaceDigitalTwin(TwinContents.Id, TwinContents);

            await Relationshipbuilder.UpdateRelationshipAsync(Client, TwinContents.Id, TargetId, RelationName);

        }

        /// <summary>Creates the sykkel twin asynchronous.</summary>
        /// <param name="Station_Information">The station information. <see cref="Station_Information"/></param>
        /// <param name="IdNavn">The identifier navn.</param>
        /// <param name="Station_Status">The station status. <see cref="Station_Status"/></param>
        private async void CreateSykkelTwinAsync(Station_Information Station_Information, string IdNavn, Station_Status Station_Status)
        {
            SykkelTwin Twins = new SykkelTwin();

            BasicDigitalTwin Contents = Twins.CreateSykkelTwinContents(Station_Information, IdNavn, Station_Status);

            Twins.CreateTwinsAsync(Client, Contents);

            await Relationshipbuilder.UpdateRelationshipAsync(Client, Contents.Id, "Oslo_Sykler", "sykkler_har_sykkel");

        }

    }
}
