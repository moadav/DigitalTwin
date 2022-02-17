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
        public async static Task CreateRelationshipAsync(DigitalTwinsClient client, string digitalTwinSrcId, string digitalTwinTargetId,string nameId)
        {          
            try
            {
                string relId = $"{digitalTwinSrcId}{nameId}{digitalTwinTargetId}";
                await client.CreateOrReplaceRelationshipAsync(digitalTwinSrcId, relId,  BasicRelationshipCreator(digitalTwinTargetId,nameId));
                Console.WriteLine("Created relationship successfully");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Create relationship error: {e.Status}: {e.Message}");
            }
        }
        

        private static BasicRelationship BasicRelationshipCreator(string targetId,string nameId)
        {
            var relationship = new BasicRelationship
            {
                TargetId = targetId,
                Name = nameId
            };
            return relationship;
        }



    }
}
