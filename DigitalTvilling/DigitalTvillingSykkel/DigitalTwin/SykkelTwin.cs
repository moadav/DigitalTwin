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
        readonly string sykkel_tilgjengelighet_modelId = "dtmi:oslo:sykler:stasjoner;1";
        readonly string sykkel_status_property_name = "Station_Status";
        readonly string sykkel_tilgjengelighet_property_name = "Station_information";
        readonly string sykkel_tilgjengelighet_component_name = "Bicycle_Status";
        public BasicDigitalTwin CreateSykkelTwinContents(Station_Information station_Information, string idNavn, Station_Status station_Status)
        {
            
            var twinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = sykkel_tilgjengelighet_modelId
                },
                Contents =
                {
                    {sykkel_tilgjengelighet_property_name,  station_Information},
                    { sykkel_tilgjengelighet_component_name, new BasicDigitalTwinComponent { Contents ={  { sykkel_status_property_name, station_Status } } } }

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
                Console.WriteLine($"Failed to send request: {e.Status}: {e.Message}");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine($"Null value recieved: " + e);
            }

        }

        private async void DeleteTwinAsync(DigitalTwinsClient client, string twinId)
        {
            try
            {
                await client.DeleteDigitalTwinAsync(twinId);
                Console.WriteLine($"Twin {twinId} deleted ");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Failed to find twin: " + e);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Null value recieved: " + e);
            }
        }

        private async void UpdateKlimaTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
        {
            try
            {
                var updateTwins = new JsonPatchDocument();

                updateTwins.AppendReplace("/" + sykkel_tilgjengelighet_component_name, basicDigitalTwin.Contents[sykkel_tilgjengelighet_component_name]);
                updateTwins.AppendReplace("/" + sykkel_tilgjengelighet_property_name, basicDigitalTwin.Contents[sykkel_tilgjengelighet_property_name]);

                await client.UpdateDigitalTwinAsync(basicDigitalTwin.Id, updateTwins);

                Console.WriteLine($"Digital twin {basicDigitalTwin.Id} updated succesfully");
            }
            catch (RequestFailedException e)
            {
                if (e.Status == 404)
                    CreateNewSykkelTwinsAsync(client, basicDigitalTwin);
                else
                    Console.WriteLine($"Failed to send request: {e.Status}: {e.Message}");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine($"Value Null recieved: " + e);
            }

        }

        public void CreateTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
        {

            UpdateKlimaTwinsAsync(client, basicDigitalTwin);

        }




    }
}
