using Azure;
using Azure.DigitalTwins.Core;
using DigitalTvillingSykkel.SykkelTvillingObjekter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTvillingSykkel.DigitalTwin
{

    /// <summary>Class that handles the digital twin logic for bicycle twin</summary>
    public class SykkelTwin
    {



        /// <summary>The sykkel tilgjengelighet DTDL - model identifier</summary>
        readonly string Sykkel_Tilgjengelighet_ModelId = "dtmi:oslo:sykler:stasjoner;1";


        /// <summary>The sykkel status property "name"</summary>
        readonly string Sykkel_Status_Property_Name = "Station_Status";


        /// <summary>The sykkel tilgjengelighet property "name"</summary>
        readonly string Sykkel_Tilgjengelighet_Property_Name = "Station_information";


        /// <summary>The sykkel tilgjengelighet "component" name</summary>
        readonly string Sykkel_Tilgjengelighet_Component_Name = "Bicycle_Status";


        /// <summary>Creates the sykkel twin contents.</summary>
        /// <param name="Station_Information">The station Station_Information class <see cref="Station_Information"/></param>
        /// <param name="IdNavn">The identifier navn.</param>
        /// <param name="Station_Status">The Station_Status class <see cref="Station_Status"/></param>
        /// <returns>A twin</returns>
        public BasicDigitalTwin CreateSykkelTwinContents(Station_Information Station_Information, string IdNavn, Station_Status Station_Status)
        {
            
            var twinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = Sykkel_Tilgjengelighet_ModelId
                },
                Contents =
                {
                    {Sykkel_Tilgjengelighet_Property_Name,  Station_Information},
                    { Sykkel_Tilgjengelighet_Component_Name, new BasicDigitalTwinComponent { Contents ={  { Sykkel_Status_Property_Name, Station_Status } } } }

                },
                Id = IdNavn
            };
            return twinContents;
        }



        /// <summary>Creates a sykkel twin asynchronous.</summary>
        /// <param name="client">The DigitalTwinsClient <see cref="DigitalTwinsClient"/></param>
        /// <param name="basicDigitalTwin">The BasicDigitalTwin <see cref="BasicDigitalTwin"/></param>
        private async void CreateNewSykkelTwinAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
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

        /// <summary>Deletes a twin asynchronous.</summary>
        /// <param name="client">The client. <see cref="DigitalTwinsClient"/></param>
        /// <param name="twinId">The twin identifier.</param>
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

        /// <summary>Updates the sykkel twin asynchronous.</summary>
        /// <param name="client">The client. <see cref="DigitalTwinsClient"/></param>
        /// <param name="basicDigitalTwin">The basic digital twin. <see cref="BasicDigitalTwin"/></param>
        private async void UpdateSykkelTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
        {
            try
            {
                var updateTwins = new JsonPatchDocument();

                updateTwins.AppendReplace("/" + Sykkel_Tilgjengelighet_Component_Name, basicDigitalTwin.Contents[Sykkel_Tilgjengelighet_Component_Name]);
                updateTwins.AppendReplace("/" + Sykkel_Tilgjengelighet_Property_Name, basicDigitalTwin.Contents[Sykkel_Tilgjengelighet_Property_Name]);

                await client.UpdateDigitalTwinAsync(basicDigitalTwin.Id, updateTwins);

                Console.WriteLine($"Digital twin {basicDigitalTwin.Id} updated succesfully");
            }
            catch (RequestFailedException e)
            {
                if (e.Status == 404)
                    CreateNewSykkelTwinAsync(client, basicDigitalTwin);
                else
                    Console.WriteLine($"Failed to send request: {e.Status}: {e.Message}");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine($"Value Null recieved: " + e);
            }

        }

        /// <summary>Runs the method to create twin asynchronous.</summary>
        /// <param name="client">The client. <see cref="DigitalTwinsClient"/></param>
        /// <param name="basicDigitalTwin">The basic digital twin. <see cref="BasicDigitalTwin"/></param>
        public void CreateTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin)
        {
            UpdateSykkelTwinsAsync(client, basicDigitalTwin);

        }




    }
}
