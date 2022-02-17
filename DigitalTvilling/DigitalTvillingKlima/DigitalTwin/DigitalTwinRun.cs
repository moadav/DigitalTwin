using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Hjelpeklasser;
using DigitalTvillingKlima.Interface;
using DigitalTvillingKlima.testfolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace DigitalTvillingKlima.DigitalTwin
{
    public class DigitalTwinRun
    {

        List<Coordinates> koordinater = new List<Coordinates>();

        public void Run()
        {
            Api.InitalizeApi();

            DigitalTwinsClient client = DigitalTwinsInstansiateClient.DigitalTwinsClient(new Uri("https://dthiofadt.api.weu.digitaltwins.azure.net"));

            JsonToModel.InitializeModels(client);

            /*
                "dtmi:omrade:sted;1"
                "dtmi: oslo: by; 1"
                "dtmi:omrade:klima;1"

            */


            Console.WriteLine("completed");


            Apie();

                DigitalTwinsOmrade twins = new DigitalTwinsOmrade();
                 BasicDigitalTwin contents = twins.CreateOmradeTwinContents(new KlimaInfo("CLOUDY", "10:00:12", new Air_info(32, 22, 12)
                    , new Wind_info(22, 22, 22)), "luftHavn", new Coordinates(22.1,331.2,"a"));
              twins.CreateTwinsAsync(client, contents);

        }

        private void Koordinaterverdi()
        {
            koordinater.Add(new Coordinates(59.9268, 10.7162, "Majorstuen"));
            koordinater.Add(new Coordinates(59.9167, 10.7068, "Frogner"));
            koordinater.Add(new Coordinates(59.9261, 10.7757, "Grünerlokka"));
            koordinater.Add(new Coordinates(59.9380, 10.7363, "ulleval"));
            koordinater.Add(new Coordinates(59.9411, 10.8199, "Bjerke"));
            koordinater.Add(new Coordinates(59.9127, 10.7318, "Sentrum"));
            koordinater.Add(new Coordinates(59.9068, 10.7623, "Gamle Oslo"));
        }

        public async void Apie()
        {
            

            



            using (HttpResponseMessage response = await Api.Client.GetAsync( $"https://api.met.no/weatherapi/locationforecast/2.0/compact.json?lat=59.1248&lon={11.3875}"))
            {

                

                    if (response.IsSuccessStatusCode)
                    {
                        Feature getWeather = await response.Content.ReadAsAsync<Feature>();

                        Console.WriteLine(getWeather.Properties.TimeSeries[1].Data.Instant.Details.Air_pressure_at_sea_level);
                        Console.WriteLine(getWeather.Properties.TimeSeries[1].Data.Instant.Details.Air_pressure_at_sea_level);
                        Console.WriteLine(getWeather.Properties.TimeSeries[1].Data.Instant.Details.Air_pressure_at_sea_level);
                        Console.WriteLine(getWeather.Properties.TimeSeries[1].Data.Instant.Details.Air_pressure_at_sea_level);
                        Console.WriteLine(getWeather.Properties.TimeSeries[1].Data.Instant.Details.Air_pressure_at_sea_level);
                        Console.WriteLine(getWeather.Properties.TimeSeries[1].Data.Instant.Details.Air_pressure_at_sea_level);
                        Console.WriteLine(getWeather.Properties.TimeSeries[1].Data.Instant.Details.Air_pressure_at_sea_level);
                        Console.WriteLine(getWeather.Properties.TimeSeries[1].Data.Instant.Details.Air_pressure_at_sea_level);

                    }




            }
        }


    }
}
