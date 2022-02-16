using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Hjelpeklasser;
using DigitalTvillingKlima.Interface;
using DigitalTvillingKlima.testfolder;
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


            DigitalTwinsOmrade twins = new DigitalTwinsOmrade();
       //     BasicDigitalTwin contents = twins.CreateOmradeTwinContents(new KlimaInfo("CLOUDY", "10:00:12", new Air_info(32, 22, 12)
        //        , new Wind_info(22, 22, 22)), "luftHavn");
         //   twins.CreateTwinsAsync(client, contents);

        }


    }
}
