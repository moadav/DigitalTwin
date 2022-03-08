using Azure;
using Azure.DigitalTwins.Core;
using DigitalTvillingSykkel.SykkelTvillingObjekter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.DigitalTwin
{
    public class SykkelTwin
    {

        public BasicDigitalTwin CreateSykkelTwinContents(Station_Information station_Information, string idNavn, Station_Status station_Status)
        {
            var twinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = "dtmi:oslo:sykkler:sykkel;1"
                },
                Contents =
                {
                    {"Station_information",  station_Information},
                    { "Bicycle_Status", new BasicDigitalTwinComponent { Contents ={  {"Station_Status", station_Status } } } }

                },
                Id = idNavn


            };

            return twinContents;
        }



        private async void CreateNewSykkelTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
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


                updateTwins.AppendReplace("/Bicycle_Status", basicDigitalTwin.Contents["Bicycle_Status"]);
                updateTwins.AppendReplace("Station_information", basicDigitalTwin.Contents["Station_information"]);

                await client.UpdateDigitalTwinAsync(basicDigitalTwin.Id, updateTwins);

                Console.WriteLine($"Digital twin {basicDigitalTwin.Id} updated succesfully");
            }
            catch (RequestFailedException e)
            {
                if (e.Status == 404)
                    CreateNewSykkelTwinsAsync(client, basicDigitalTwin);
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
