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
    public class DigitalTwinsOmrade : IDigitalTwinsBuilder
    {


        public BasicDigitalTwin CreateOmradeTwinContents(KlimaInfo klimaInfo, string idNavn)
        {
            var twinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = "dtmi:omrade:sted;1"
                },
                Contents =
                {
                    {"stedNavn", idNavn},
                    { "weather", test(klimaInfo) }

                },
                Id = idNavn


            };

            return twinContents;
        }

        private BasicDigitalTwinComponent test(KlimaInfo klimaInfo)
        {

            var tester = new BasicDigitalTwinComponent
            {
            
                Contents =
                {
                    {"KlimaInfo", klimaInfo }
                }
            };



            return tester;


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
