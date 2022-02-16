using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima.Hjelpeklasser
{
    public class Relationshipbuilder
    {
        public async static Task CreateRelationshipAsync(DigitalTwinsClient client, string digitalTwinSrcId, string digitalTwinTargetId, string relname)
        {          
            try
            {
                string relId = $"{digitalTwinSrcId}{relname}{digitalTwinTargetId}";
                await client.CreateOrReplaceRelationshipAsync(digitalTwinSrcId, relId,  BasicRelationshipCreator(digitalTwinTargetId));
                Console.WriteLine("Created relationship successfully");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Create relationship error: {e.Status}: {e.Message}");
            }
        }

        private static BasicRelationship BasicRelationshipCreator(string targetId)
        {
            var relationship = new BasicRelationship
            {
                TargetId = targetId,
                Name = "har_klima"
            };
            return relationship;
        }



    }
}
