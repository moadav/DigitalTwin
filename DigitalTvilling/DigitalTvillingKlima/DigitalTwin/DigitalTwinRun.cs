using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Hjelpeklasser;
using DigitalTvillingKlima.Interface;
using DigitalTvillingKlima.testfolder;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace DigitalTvillingKlima.DigitalTwin
{
    public class DigitalTwinRun
    {

        private List<Coordinates> Koordinater = new List<Coordinates>();
        DigitalTwinsClient Client = DigitalTwinsInstansiateClient.DigitalTwinsClient(new Uri("https://dthiofadt.api.weu.digitaltwins.azure.net"));

        private string Weathersymbol { get; set; }
        private string Time { get; set; }
        private double Air_pressure { get; set; }
        private double Air_temp { get; set; }
        private double Cloud_frac { get; set; }
        private double Relative_hum { get; set; }
        private double Wind_dir { get; set; }
        private double Wind_speed { get; set; }



        public void Run()
        {
            Koordinaterverdi();
            Api.InitalizeKlimaApi();

            //skaper error
            //JsonToModel.InitializeModels(Client);

            CreateTwins();
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

        private async void CreateTwins()
        {
            for (int i = 0; i < Koordinater.Count; i++)
            {
                string latGrammar = Koordinater[i].Lat.ToString("G", CultureInfo.InvariantCulture);
                string lonGrammar = Koordinater[i].Lon.ToString("G", CultureInfo.InvariantCulture);

                using (HttpResponseMessage response = await Api.Client.GetAsync($"https://api.met.no/weatherapi/locationforecast/2.0/compact.json?lat={latGrammar}&lon={lonGrammar}"))
                {
                    ReadResponse(response, i);
                }
            }
        }

        private async void ReadResponse(HttpResponseMessage response, int loop)
        {
            if (response.IsSuccessStatusCode)
            {

                Feature getWeather = await response.Content.ReadAsAsync<Feature>();


                giveValues(getWeather, 0);

                KlimaInfo klimaInfo = new KlimaInfo(Weathersymbol, Time, new Air_info(Air_pressure, Air_temp, Cloud_frac)
                   , new Wind_info(Relative_hum, Wind_dir, Wind_speed));

                Coordinates coordinates = new Coordinates(Koordinater[loop].Lat, Koordinater[loop].Lon, Koordinater[loop].StedNavn);


                CreateTwinToAzure(klimaInfo, coordinates);
            }
      
        }

        private async void CreateTwinToAzure(KlimaInfo klimaInfo, Coordinates coordinates)
        {
            DigitalTwinsOmrade twins = new DigitalTwinsOmrade();

            BasicDigitalTwin contents = twins.CreateOmradeTwinContents(klimaInfo, coordinates.StedNavn, coordinates);

            twins.CreateTwinsAsync(Client, contents);

            await Relationshipbuilder.CreateRelationshipAsync(Client, contents.Id, "Oslo", "Oslo_har_bydel");

        }

   
        private void giveValues(Feature getWeather, int index)
        {
            string aTime = getWeather.Properties.TimeSeries[0].Time;
            DateTime date = DateTime.Parse(aTime, System.Globalization.CultureInfo.CurrentCulture);


            if (DateTime.Now >= date)        
                index = 1;
            

            Weathersymbol = getWeather.Properties.TimeSeries[index].Data.Next_1_hours?.Summary.Symbol_code;
            Time = getWeather.Properties.TimeSeries[index].Time;
            Air_pressure = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Air_pressure_at_sea_level;
            Air_temp = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Air_temperature;
            Cloud_frac = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Cloud_area_fraction;
            Relative_hum = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Relative_humidity;
            Wind_dir = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Wind_from_direction;
            Wind_speed = getWeather.Properties.TimeSeries[index].Data.Instant.Details.Wind_speed;



        }



    }
}
