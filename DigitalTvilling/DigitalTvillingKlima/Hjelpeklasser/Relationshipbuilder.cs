using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima.Hjelpeklasser
{
    public static class Relationshipbuilder
    {
        public async static Task UpdateRelationshipAsync(DigitalTwinsClient client, string digitalTwinSrcId, string digitalTwinTargetId, string nameId)
        {
            string relId = $"{digitalTwinSrcId}-{nameId}-{digitalTwinTargetId}";
         
            try
            {
                var updateRelationship = new JsonPatchDocument();

                await client.UpdateRelationshipAsync(digitalTwinSrcId, relId, updateRelationship);
                
                Console.WriteLine("Updated relationship successfully");
            }
            catch (RequestFailedException e)
            {
                if (e.Status == 404)
                    CreateNewRelationship(client, digitalTwinSrcId, relId, digitalTwinTargetId, nameId);
                else
                    Console.WriteLine($"Update relationship error: {e.Status}: {e.Message}");
            }
            catch (ArgumentNullException k)
            {
                Console.WriteLine("Value is null " + k);
            }
        }

        private static async void CreateNewRelationship(DigitalTwinsClient client, string digitalTwinSrcId, string relId, string digitalTwinTargetId, string nameId)
        {
            try
            {
                await client.CreateOrReplaceRelationshipAsync(digitalTwinSrcId, relId, BasicRelationshipCreator(digitalTwinTargetId, nameId));
                Console.WriteLine("Created relationship successfully");
            }
            catch (RequestFailedException e)
            {

                Console.WriteLine($"Create relationship error: {e.Status}: {e.Message}");
            }
            catch (ArgumentNullException k)
            {
                Console.WriteLine("Value is null " + k);
            }
        }
        private static async void DeleteRelationship(DigitalTwinsClient client, string relId, string twinId)
        {
            try
            {
                await client.DeleteRelationshipAsync(twinId, relId);
                Console.WriteLine($"Relationship {relId} Deleted: " + twinId);
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine("Failed to delete: " + e);
            }
        }

        private static BasicRelationship BasicRelationshipCreator(string targetId, string nameId)
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
