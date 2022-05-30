using System;
using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.DigitalTwin;
using DigitalTvillingKlima.Interface;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DigitalTvillingKlima
{
    /// <summary>
    ///  Class that initializes the API's and creates or updates twins at specific execution times
    /// </summary>
    public static class DigitalTwinWeather
    { 
        [FunctionName("KlimaData")]
        public  static void Run([TimerTrigger("0 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            
                DigitalTwinRun digitalTwinRun = new DigitalTwinRun();
                digitalTwinRun.Run();
            


        }
    }
}
