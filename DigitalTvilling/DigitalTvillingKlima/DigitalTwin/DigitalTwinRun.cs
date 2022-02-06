using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Hjelpeklasser;
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

            /*
                "dtmi:omrade:sted;1"
                "dtmi: oslo: by; 1"
                "dtmi:omrade:klima;1"

            */


            Console.WriteLine("completed");


            DigitalTwinsOmradeKlima twins = new DigitalTwinsOmradeKlima();
            BasicDigitalTwin contents = twins.CreateTwinContents("dtmi:omrade:klima;1");
            twins.CreateTwins(client,contents);
       


        }


    }
}
