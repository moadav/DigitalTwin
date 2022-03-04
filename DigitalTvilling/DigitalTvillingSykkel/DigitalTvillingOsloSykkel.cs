using System;
using DigitalTvillingSykkel.DigitalTwinRun;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DigitalTvillingSykkel
{
    public static class DigitalTvillingOsloSykkel
    {
        [FunctionName("SykkelData")]
        public static void Run([TimerTrigger("*/10 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            DigitalTwinSykkelRun a = new DigitalTwinSykkelRun();
            a.RunSykkel();
            
        }
    }
}
