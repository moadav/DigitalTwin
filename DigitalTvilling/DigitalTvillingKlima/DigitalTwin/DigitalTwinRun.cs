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
    public class DigitalTwinRun
    {

        private List<Coordinates> Koordinater = new List<Coordinates>();
        DigitalTwinsClient Client = DigitalTwinsInstansiateClient.DigitalTwinsClient(new Uri(Api.azureUrl));
        
        private string Weathersymbol { get; set; }
        private string Time { get; set; }
        private double Air_pressure { get; set; }
        private double Air_temp { get; set; }
        private double Cloud_frac { get; set; }
        private double Relative_hum { get; set; }
        private double Wind_dir { get; set; }
        private double Wind_speed { get; set; }
        private double Precipitation_amount { get; set; }
        public void Run()
        {

            Koordinaterverdi();
            Api.InitalizeKlimaApi();

            //skaper error
            //JsonToModel.InitializeModels(Client);

            CreateTwinsAsync();
            Console.WriteLine("completed");





        }


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

        private async void CreateTwinsAsync()
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

        private async void ReadResponseAsync(HttpResponseMessage response, int loop)
        {
            var content = await response.Content.ReadAsStringAsync();
            Feature getWeather = JsonConvert.DeserializeObject<Feature>(content);

            GiveValues(getWeather, 1);

            CreateTwin(loop);
        }

        private void CreateTwin(int loop)
        {
            KlimaInfo klimaInfo = new KlimaInfo(Weathersymbol, Time, new Air_info(Air_pressure, Air_temp, Cloud_frac, Precipitation_amount)
                  , new Wind_info(Relative_hum, Wind_dir, Wind_speed));

            Coordinates coordinates = new Coordinates(Koordinater[loop].Lat, Koordinater[loop].Lon, Koordinater[loop].StedNavn);


            CreateTwinToAzureAsync(klimaInfo, coordinates);



        }

        private async void CreateTwinToAzureAsync(KlimaInfo klimaInfo, Coordinates coordinates)
        {

            DigitalTwinsOmrade twins = new DigitalTwinsOmrade();

            BasicDigitalTwin contents = twins.CreateOmradeTwinContents(klimaInfo, coordinates.StedNavn, coordinates);

            twins.CreateTwinsAsync(Client, contents);

            string osloTwinId = "Oslo";
            string relId = "Oslo_har_bydel";

            await Relationshipbuilder.UpdateRelationshipAsync(Client, contents.Id, osloTwinId, relId);
        }


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
    }
}
