using Azure;
using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Interface;
using DigitalTvillingKlima.testfolder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima.Hjelpeklasser
{
    public class DigitalTwinsOmradeKlima : IDigitalTwinsBuilder
    {


        public BasicDigitalTwin CreateKlimaTwinContents(string modelId, KlimaInfo klimaInfo, string navn, string idNavn)
        {
            var twinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = modelId
                },
                Contents =
                {
                    { "name", navn },
                    { "KlimaInfo", klimaInfo }
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
                updateTwins.AppendReplace("/name", basicDigitalTwin.Contents["name"]);
                updateTwins.AppendReplace("/KlimaInfo", basicDigitalTwin.Contents["KlimaInfo"]);
                await client.UpdateDigitalTwinAsync(basicDigitalTwin.Id, updateTwins);

                Console.WriteLine($"Digital twin {basicDigitalTwin.Id} updated succesfully");
            }
            catch (RequestFailedException e)
            {
                if(e.Status == 404)
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
