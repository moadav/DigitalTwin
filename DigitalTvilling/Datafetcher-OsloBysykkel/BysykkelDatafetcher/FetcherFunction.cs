using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BysykkelDatafetcher.utils;


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

    public class FetchDataAndUpdate
    {
        public async Task RunAsync(ILogger log)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string timestamp = DateTimeOffset.UtcNow.DateTime.ToLocalTime().ToString("[dd-MM HH:mm]");

            // id => station
            Dictionary<string, Station> stationMap = new Dictionary<string, Station>();
            try
            {
                // station_information
                using (HttpResponseMessage response = await httpClient.GetAsync("https://gbfs.urbansharing.com/oslobysykkel.no/station_information.json"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        StationInformation stationInformation = await response.Content.ReadAsAsync<StationInformation>();

                        foreach (var station in stationInformation.data.stations)
                        {
                            stationMap.Add(station.station_id, station);
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"{timestamp} station_information error code: {response.StatusCode}");
                        log.LogInformation($"{timestamp} station_information error code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                log.LogInformation(e.Message);
            }

            try
            {
                // station_status.json
                using (HttpResponseMessage response = await httpClient.GetAsync("https://gbfs.urbansharing.com/oslobysykkel.no/station_status.json"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var db = new StationContext())
                        {

                            StationInformation stationStatus = await response.Content.ReadAsAsync<StationInformation>();

                            Console.WriteLine($"{timestamp} INSERTING {stationStatus.data.stations.Count} stations");
                            foreach (var station in stationStatus.data.stations)
                            {

                                Station info = stationMap[station.station_id];

                                station.name = info.name;
                                station.address = info.address;
                                station.lat = info.lat;
                                station.lon = info.lon;
                                station.capacity = info.capacity;

                                // debugging: print obj 
                                var jsonString = JsonConvert.SerializeObject(
                                   station, Formatting.Indented,
                                   new JsonConverter[] { new StringEnumConverter() });
                                Console.WriteLine($"{timestamp} INSERT ");
                                Console.WriteLine(jsonString);

                                // insert into database
                                db.Stations.Add(station);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{timestamp} station_status error code: {response.StatusCode}");
                        log.LogInformation($"{timestamp} station_status error code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                log.LogInformation(e.Message);
            }
        }
    }
}
