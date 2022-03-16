using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;


namespace BysykkelDatafetcher
{
    public static class FetcherFunction
    {
        [FunctionName("FetcherFunction")]
        public static async Task RunAsync([TimerTrigger("0 0 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"BysykkelDatafetcher function executed at: {DateTime.Now}");

            await new FetchDataAndUpdate().RunAsync(log);
        }
    }
}
