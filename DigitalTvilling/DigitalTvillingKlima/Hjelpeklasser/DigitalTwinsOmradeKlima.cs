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
        

        public BasicDigitalTwin CreateTwinContents(string modelId)
        {

          

          
            var twinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = modelId
                },
                Contents =
                {
                    {"name", "Oslo" },
                    {"KlimaInfo",  new KlimaInfo("TEST","2TESTER",new Air_info(2.2,2.1,2),new Wind_info(2,2,2)) }
                    
                                
                    
                }
            };

            return twinContents;
        }

        public async void CreateTwins(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
        {
            try
            {
                basicDigitalTwin.Id = "231ttte";

                await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(basicDigitalTwin.Id, basicDigitalTwin);

                Console.WriteLine($"Created twin: {basicDigitalTwin.Id}");
              
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Create twin error: {e.Status}: {e.Message}");
            }
        }
    }
}
