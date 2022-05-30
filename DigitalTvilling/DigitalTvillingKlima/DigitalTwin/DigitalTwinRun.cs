using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Hjelpeklasser;
using DigitalTvillingKlima.testfolder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;


namespace DigitalTvillingKlima.DigitalTwin
{

    /// <summary>Class that runs the digital twin logic for climate</summary>
    public class DigitalTwinRun
    {

        /// <summary>A list of coordinates to the Oslo districts</summary>
        private List<Coordinates> Koordinater = new List<Coordinates>();

        /// <summary>The DigitalTwinsClient which connects to the Azure Digital Twin plattform</summary>
        private readonly DigitalTwinsClient Client = DigitalTwinsInstansiateClient.DigitalTwinsClient();

        /// <summary>Gets or sets the weathersymbol.</summary>
        /// <value>The weathersymbol.</value>
        private string Weathersymbol { get; set; }

        /// <summary>Gets or sets the time.</summary>
        /// <value>The time.</value>
        private string Time { get; set; }

        /// <summary>Gets or sets the air pressure.</summary>
        /// <value>The air pressure.</value>
        private double Air_pressure { get; set; }

        /// <summary>Gets or sets the air temporary.</summary>
        /// <value>The air temporary.</value>
        private double Air_temp { get; set; }

        /// <summary>Gets or sets the cloud frac.</summary>
        /// <value>The cloud frac.</value>
        private double Cloud_frac { get; set; }


        /// <summary>Gets or sets the relative hum.</summary>
        /// <value>The relative hum.</value>
        private double Relative_hum { get; set; }


        /// <summary>Gets or sets the wind dir.</summary>
        /// <value>The wind dir.</value>
        private double Wind_dir { get; set; }


        /// <summary>Gets or sets the wind speed.</summary>
        /// <value>The wind speed.</value>
        private double Wind_speed { get; set; }


        /// <summary>Gets or sets the precipitation amount.</summary>
        /// <value>The precipitation amount.</value>
        private double Precipitation_amount { get; set; }


        /// <summary>Runs the logic of creating the Digital Twins for the weather</summary>
        public void Run()
        {

            Koordinaterverdi();
            Api.InitalizeKlimaApi();

            GetApiValues();
            Console.WriteLine("completed");





        }


        /// <summary>Adds the oslo districts to a list</summary>
        private void Koordinaterverdi()
        {
            Koordinater.Clear();

            Koordinater.Add(new Coordinates(59.8911, 10.8315, "Oosten-sjoo"));

            Koordinater.Add(new Coordinates(59.8345, 10.8196, "Soondre-Nordstrand"));

            Koordinater.Add(new Coordinates(59.9606, 10.9222, "Stovner"));

            Koordinater.Add(new Coordinates(59.9290, 10.7403, "Hanshaugen"));

            Koordinater.Add(new Coordinates(59.9379, 10.7609, "Sagene"));

            Koordinater.Add(new Coordinates(59.9542, 10.7633, "Nordre-Aker"));

            Koordinater.Add(new Coordinates(59.9545, 10.8707, "Grorud"));

            Koordinater.Add(new Coordinates(59.9308, 10.8638, "Alna"));

            Koordinater.Add(new Coordinates(59.8715, 10.7913, "Nordstrand"));

            Koordinater.Add(new Coordinates(59.9167, 10.7068, "Frogner"));

            Koordinater.Add(new Coordinates(59.9261, 10.7757, "Grunerlokka"));

            Koordinater.Add(new Coordinates(59.9380, 10.7363, "Ulleval"));

            Koordinater.Add(new Coordinates(59.9411, 10.8199, "Bjerke"));

            Koordinater.Add(new Coordinates(59.9374, 10.7272, "Vestre-Aker"));

            Koordinater.Add(new Coordinates(59.9068, 10.7623, "Gamle-Oslo"));
        }


        /// <summary>Takes the oslo districts coordinates and gets the api information</summary>
        private async void GetApiValues()
        {
            for (int i = 0; i < Koordinater.Count; i++)
            {
                string latGrammar = Koordinater[i].Lat.ToString("G", CultureInfo.InvariantCulture);
                string lonGrammar = Koordinater[i].Lon.ToString("G", CultureInfo.InvariantCulture);


                try
                {
                    using (HttpResponseMessage response = await Api.Client.GetAsync($"https://api.met.no/weatherapi/locationforecast/2.0/compact.json?lat={latGrammar}&lon={lonGrammar}"))
                    {

                        response.EnsureSuccessStatusCode();
                        ReadResponseAsync(response, i);
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
        }

        /// <summary>Reads the response from API asynchronous.</summary>
        /// <param name="response">The response. <see cref="HttpResponseMessage"/></param>
        /// <param name="loop">An integer value representing the current number of the array</param>
        private async void ReadResponseAsync(HttpResponseMessage response, int loop)
        {
            var content = await response.Content.ReadAsStringAsync();
            Feature getWeather = JsonConvert.DeserializeObject<Feature>(content);

            GiveValues(getWeather, 1);

            CreateTwin(loop);
        }

        /// <summary>Creates the twin.</summary>
        /// <param name="loop">An integer value representing the current number of the array</param>
        private void CreateTwin(int loop)
        {
            KlimaInfo klimaInfo = new KlimaInfo(Weathersymbol, Time, new Air_info(Air_pressure, Air_temp, Cloud_frac, Precipitation_amount)
                  , new Wind_info(Relative_hum, Wind_dir, Wind_speed));

            Coordinates coordinates = new Coordinates(Koordinater[loop].Lat, Koordinater[loop].Lon, Koordinater[loop].StedNavn);


            CreateTwinToAzureAsync(klimaInfo, coordinates);
        }


        /// <summary>Gives the values.</summary>
        /// <param name="getWeather">Feature class which comes from the response from API <see cref="Feature"/></param>
        /// <param name="index">An integer value to skip first array value to return the real-time data</param>
        private void GiveValues(Feature getWeather, int index)
        {
            String time = Weathersymbol = getWeather.Properties.TimeSeries[index].Time;
            DateTime date = DateTime.Parse(time, CultureInfo.CurrentCulture);

            if (DateTime.Now > date)
                index++;

            Weathersymbol = getWeather.Properties.TimeSeries[index - 1].Data.Next_1_hours?.Summary.Symbol_code;
            Time = getWeather.Properties.TimeSeries[index].Time;
            Air_pressure = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Air_pressure_at_sea_level;
            Air_temp = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Air_temperature;
            Cloud_frac = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Cloud_area_fraction;
            Relative_hum = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Relative_humidity;
            Wind_dir = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Wind_from_direction;
            Wind_speed = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Wind_speed;
            Precipitation_amount = getWeather.Properties.TimeSeries[index - 1].Data.Next_1_hours.Details.Precipitation_amount;


        }

        /// <summary>Creates the twin to azure asynchronous.</summary>
        /// <param name="klimaInfo">The KlimaInfo class <see cref="KlimaInfo"/></param>
        /// <param name="coordinates">The Coordinates class <see cref="Coordinates"/></param>
        private async void CreateTwinToAzureAsync(KlimaInfo klimaInfo, Coordinates coordinates)
        {

            DigitalTwinsOmrade twins = new DigitalTwinsOmrade();

            BasicDigitalTwin contents = twins.CreateOmradeTwinContents(klimaInfo, coordinates.StedNavn, coordinates);

            twins.CreateTwinAsync(Client, contents);

            string osloTwinId = "Oslo";
            string relId = "Oslo_har_bydel";

            await Relationshipbuilder.UpdateRelationshipAsync(Client, contents.Id, osloTwinId, relId);
        }


       
    }
}
