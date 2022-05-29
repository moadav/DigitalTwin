using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima.Hjelpeklasser
{

    /// <summary>Class that helps with building the relationship</summary>
    public static class Relationshipbuilder
    {


        /// <summary>Updates the relationship asynchronous.</summary>
        /// <param name="Client">The client.</param>
        /// <param name="DigitalTwinSrcId">The digital twin source identifier.</param>
        /// <param name="DigitalTwinTargetId">The digital twin target identifier.</param>
        /// <param name="NameId">The name identifier.</param>
        public async static Task UpdateRelationshipAsync(DigitalTwinsClient Client, string DigitalTwinSrcId, string DigitalTwinTargetId, string NameId)
        {

            ///<summary>The relationship Id created for the relationship </summary>
            string relId = $"{DigitalTwinSrcId}-{NameId}-{DigitalTwinTargetId}";
         
            try
            {
                var updateRelationship = new JsonPatchDocument();

                /// <summary>Updates the relationship</summary>
                /// <param name="DigitalTwinSrcId"> The id of the source twin</param>
                /// <param name="relId"> The id of the relationship</param>
                /// <param name="updateRelationship"> The JSONPatchDocument which updates the content on the DTDL - Modell attribute (The DTDL - model relationship is empty )</param>
                await Client.UpdateRelationshipAsync(DigitalTwinSrcId, relId, updateRelationship);
                
                Console.WriteLine("Updated relationship successfully");
            }
            catch (RequestFailedException e)
            {
                if (e.Status == 404)
                    CreateNewRelationship(Client, DigitalTwinSrcId, relId, DigitalTwinTargetId, NameId);
                else
                    Console.WriteLine($"Update relationship error: {e.Status}: {e.Message}");
            }
            catch (ArgumentNullException k)
            {
                Console.WriteLine("Value is null " + k);
            }
        }


        /// <summary>Creates a new relationship.</summary>
        /// <param name="Client">The client.</param>
        /// <param name="DigitalTwinSrcId">The digital twin source identifier.</param>
        /// <param name="RelId">The rel identifier.</param>
        /// <param name="DigitalTwinTargetId">The digital twin target identifier.</param>
        /// <param name="NameId">The name identifier.</param>
        private static async void CreateNewRelationship(DigitalTwinsClient Client, string DigitalTwinSrcId, string RelId, string DigitalTwinTargetId, string NameId)
        {
            try
            {
                /// <summary>creates or replaces the relationship</summary>
                /// <param name="DigitalTwinSrcId"> The id of the source twin</param>
                /// <param name="RelId"> The id of the relationship</param>
                /// <param name="DigitalTwinTargetId"> The id of the target twin</param>
                /// <param name="NameId"> The "name" attribute of the relationship found on the DTDL - model </param>
                await Client.CreateOrReplaceRelationshipAsync(DigitalTwinSrcId, RelId, BasicRelationshipCreator(DigitalTwinTargetId, NameId));
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
