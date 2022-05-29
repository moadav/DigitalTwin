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

    /// <summary>Class that handles the creation of twins</summary>
    public class DigitalTwinsOmrade : IDigitalTwinsKlimaBuilder
    {

        /// <summary>The DTDL - modell Id</summary>
        readonly string model_Omrade_Klima_modelId = "dtmi:omrade:sted;1";

        /// <summary>The "name" of Property to access</summary>
        readonly string model_omrade_Klima_Property_Name = "KlimaInfo";

        /// <summary>The "component" of the DTDL - model with attribute value "name"</summary>
        readonly string model_omrade_digitaltwins_Component_Name = "weather";
        /// <summary>The "Property" of the DTDL - model with attribute value "name"</summary>
        readonly string model_omrade_Coordinates_Property_Name = "Coordinates";


        /// <summary>Creates the Twin with the required fields of the DTDL-model</summary>
        /// <param name="KlimaInfo">KlimaInfo Class which is used to fill the "model_omrade_Klima_Property_Name" variable with values specified in the DTDL - modell <see cref="KlimaInfo"/></param>
        /// <param name="IdNavn">The identifier of the twin created</param>
        /// <param name="Coordinates">Coordinates class which is used to fill in values for the "model_omrade_Coordinates_Property_Name" Property of the DTDL - modell <see cref="Coordinates"/></param>
        /// <returns> A Twin of type BasicDigitalTwin <see cref="BasicDigitalTwin"/></returns>
        public BasicDigitalTwin CreateOmradeTwinContents(KlimaInfo KlimaInfo, string IdNavn, Coordinates Coordinates)
        {
           

            var twinContents = new BasicDigitalTwin()
            {
                Metadata = {
                    ModelId = model_Omrade_Klima_modelId
                },
                Contents =
                {
                    { model_omrade_Coordinates_Property_Name,  Coordinates},
                    { model_omrade_digitaltwins_Component_Name, new BasicDigitalTwinComponent { Contents ={  { model_omrade_Klima_Property_Name, KlimaInfo } } } }

                },
                Id = IdNavn


            };

            return twinContents;
        }



        /// <summary>Creates the new twins asynchronous.</summary>
        /// <param name="Client">The client. <see cref="DigitalTwinsClient"/></param>
        /// <param name="BasicDigitalTwin">The Twin created <see cref="BasicDigitalTwin"/></param>
        private async void CreateNewKlimaTwinsAsync(DigitalTwinsClient Client, BasicDigitalTwin BasicDigitalTwin)
        {
            try
            {
                ///<summary> Creates a new Twin in the Azure Digital Twin plattform or replaces if it already exists</summary>
                await Client.CreateOrReplaceDigitalTwinAsync(BasicDigitalTwin.Id, BasicDigitalTwin);
                Console.WriteLine($"Created twin: {BasicDigitalTwin.Id} successfully");
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

        /// <summary>Deletes the twin.</summary>
        /// <param name="Client">The client. <see cref="DigitalTwinsClient"/></param>
        /// <param name="TwinId">The twin identifier.</param>
        private async void DeleteTwin(DigitalTwinsClient Client, string TwinId)
        {
            try
            {
                ///<summary> Deletes a Twin in the Azure Digital Twin plattform with the specified string value "twinId" in the parameter</summary>
                await Client.DeleteDigitalTwinAsync(TwinId);
                Console.WriteLine($"Twin {TwinId} deleted ");
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

        /// <summary>Updates the klima twins asynchronous.</summary>
        /// <param name="Client">The client. <see cref="DigitalTwinsClient"/></param>
        /// <param name="BasicDigitalTwin">The Twin created <see cref="BasicDigitalTwin"/></param>
        private async void UpdateKlimaTwinsAsync(DigitalTwinsClient Client, BasicDigitalTwin BasicDigitalTwin)
        {
            try
            {
               
                var updateTwins = new JsonPatchDocument();

                ///<summary>Updates the component value for the Twin specified in the parameter "BasicDigitalTwin" </summary>
                updateTwins.AppendReplace("/" + model_omrade_digitaltwins_Component_Name, BasicDigitalTwin.Contents[model_omrade_digitaltwins_Component_Name]);

                await Client.UpdateDigitalTwinAsync(BasicDigitalTwin.Id, updateTwins);

                Console.WriteLine($"Digital twin {BasicDigitalTwin.Id} updated succesfully");
            }
            catch (RequestFailedException e)
            {
                if (e.Status == 404)
                    CreateNewKlimaTwinsAsync(Client, BasicDigitalTwin);
                else
                    Console.WriteLine($"Failed to send update request: {e.Status}: {e.Message}");
            }
            catch (ArgumentNullException k)
            {
                Console.WriteLine("Null value recieved: " + k);
            }

        }

        /// <summary>Creates twin asynchronous.</summary>
        /// <param name="Client">The client. <see cref="DigitalTwinsClient"/></param>
        /// <param name="BasicDigitalTwin">The BasicDigitalTwin. <see cref="BasicDigitalTwin"/></param>
        public void CreateTwinAsync(DigitalTwinsClient Client, BasicDigitalTwin BasicDigitalTwin)
        {
            UpdateKlimaTwinsAsync(Client, BasicDigitalTwin);
        }
    }
}
