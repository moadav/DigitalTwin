using Azure;
using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Interface;
using DigitalTvillingKlima.testfolder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigitalTvillingKlima.Hjelpeklasser
{
    public class DigitalTwinsOmrade : IDigitalTwinsKlimaBuilder
    {
        readonly string model_Omrade_Klima_modelId = "dtmi:omrade:sted;1";
        readonly string omrade_Klima_Property_Name = "KlimaInfo";
        readonly string digitaltwins_Component_Name = "weather";
        public BasicDigitalTwin CreateOmradeTwinContents(KlimaInfo klimaInfo, string idNavn, Coordinates coordinates)
        {
           

            var twinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = model_Omrade_Klima_modelId
                },
                Contents =
                {
                    {"Coordinates",  coordinates},
                    { "weather", new BasicDigitalTwinComponent { Contents ={  { omrade_Klima_Property_Name, klimaInfo } } } }

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
                Console.WriteLine($"failed to send create request: {e.Status}: {e.Message}");
            }
            catch (ArgumentNullException k)
            {
                Console.WriteLine("Null value recieved " + k);
            }

        }

        private async void DeleteTwin(DigitalTwinsClient client, string twinId)
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

                updateTwins.AppendReplace("/" + digitaltwins_Component_Name, basicDigitalTwin.Contents[digitaltwins_Component_Name]);

                await client.UpdateDigitalTwinAsync(basicDigitalTwin.Id, updateTwins);

                Console.WriteLine($"Digital twin {basicDigitalTwin.Id} updated succesfully");
            }
            catch (RequestFailedException e)
            {
                if (e.Status == 404)
                    CreateNewKlimaTwinsAsync(client, basicDigitalTwin);
                else
                    Console.WriteLine($"Failed to send update request: {e.Status}: {e.Message}");
            }
            catch (ArgumentNullException k)
            {
                Console.WriteLine("Null value recieved: " + k);
            }

        }

        public void CreateTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
        {
            UpdateKlimaTwinsAsync(client, basicDigitalTwin);
        }
    }
}
