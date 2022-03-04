﻿using Azure;
using Azure.DigitalTwins.Core;
using DigitalTvillingSykkel.SykkelTvillingObjekter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.DigitalTwin
{
    public class SykkelTwin
    {

        public BasicDigitalTwin CreateOmradeTwinContents(Station_Information klimaInfo, string idNavn, Station_Availablity coordinates)
        {
            var twinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = "dtmi:omrade:sted;1"
                },
                Contents =
                {
                    {"Coordinates",  coordinates},
                    { "weather", new BasicDigitalTwinComponent { Contents ={  {"KlimaInfo", klimaInfo } } } }

                },
                Id = idNavn


            };

            return twinContents;
        }



        private async void CreateNewKlimaTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
        {
            try
            {

                await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(basicDigitalTwin.Id, basicDigitalTwin);
                Console.WriteLine($"Created twin: {basicDigitalTwin.Id} successfully");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Create twin error: {e.Status}: {e.Message}");
            }

        }

        private async void UpdateKlimaTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
        {
            try
            {

                var updateTwins = new JsonPatchDocument();


                updateTwins.AppendReplace("/weather", basicDigitalTwin.Contents["weather"]);

                await client.UpdateDigitalTwinAsync(basicDigitalTwin.Id, updateTwins);

                Console.WriteLine($"Digital twin {basicDigitalTwin.Id} updated succesfully");
            }
            catch (RequestFailedException e)
            {
                if (e.Status == 404)
                    CreateNewKlimaTwinsAsync(client, basicDigitalTwin);
                else
                    Console.WriteLine($"Update twin error: {e.Status}: {e.Message}");
            }

        }

        public void CreateTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
        {
            try
            {

                UpdateKlimaTwinsAsync(client, basicDigitalTwin);
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Create twin error: {e.Status}: {e.Message}");
            }
        }




    }
}
