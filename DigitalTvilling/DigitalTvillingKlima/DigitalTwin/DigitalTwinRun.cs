using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace DigitalTvillingKlima.DigitalTwin
{
   public class DigitalTwinRun
    {

        public void Run()
        {
            Api.InitalizeApi();

            DigitalTwinsClient client = DigitalTwinsInstansiateClient.DigitalTwinsClient(new Uri("https://dthiofadt.api.weu.digitaltwins.azure.net"));

            JsonToModel.InitializeModels(client);
            

         
            Console.WriteLine("completed");



        }


    }
}
