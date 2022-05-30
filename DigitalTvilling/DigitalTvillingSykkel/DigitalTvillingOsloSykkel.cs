using System;
using DigitalTvillingSykkel.DigitalTwinRun;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DigitalTvillingSykkel
{
    /// <summary>
    ///  Class that initializes the API's and creates or updates twins at specific execution times
    /// </summary>
    public static class DigitalTvillingOsloSykkel
    {
        [FunctionName("SykkelData")]
        public static void Run([TimerTrigger("0 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            DigitalTwinSykkelRun a = new DigitalTwinSykkelRun();
            a.RunSykkel();
            
        }
    }
}
