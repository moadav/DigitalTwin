using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DigitalTvillingKlima.DigitalTwin
{
   public class DigitalTwinRun
    {

        public void Run()
        {
            Api.InitalizeApi();

            Uri uri = new Uri(Api.azureUrl);
            HttpClient httpClient = Api.singletonHttpClientInstance;

            DigitalTwinsInstansiateClient.DigitalTwinsClient(uri, httpClient);
            

        }


    }
}
