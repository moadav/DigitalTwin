using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BysykkelDatafetcher.utils;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Globalization;

namespace BysykkelDatafetcher
{
    public class FetchDataAndUpdate
    {
        HttpClient httpClient { get; set; }

        public async Task RunAsync(ILogger log)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "hiof.no - bachelorprosject");
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
                        Console.WriteLine($"{timestamp} station_information error code: {response.StatusCode}");
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
                                //merge values from station_information with stations from station_status
                                Station info = stationMap[station.station_id];

                                station.name = info.name;
                                station.address = info.address;
                                station.lat = info.lat;
                                station.lon = info.lon;
                                station.capacity = info.capacity;

                                
                                var weatherPoint = await getWeather(log, station.lat, station.lon);
                                if(weatherPoint != null)
                                {
                                    station.weatherPoint = weatherPoint;

                                    // debugging: print obj 
                                    //var jsonString = JsonConvert.SerializeObject(
                                    //   station, Formatting.Indented,
                                    //   new JsonConverter[] { new StringEnumConverter() });
                                    //Console.WriteLine($"{timestamp} INSERT ");
                                    //Console.WriteLine(jsonString);

                                    // insert into database
                                    db.Stations.Add(station);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine($"{timestamp} Could not get weather");
                                    log.LogInformation($"{timestamp} Could not get weather");
                                }
                                
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
        private async Task<WeatherPoint> getWeather(ILogger log, double latitude, double longitude)
        {
            string timestamp = DateTimeOffset.UtcNow.DateTime.ToLocalTime().ToString("[dd-MM HH:mm]");

            string latGrammar = latitude.ToString("G", CultureInfo.InvariantCulture);
            string lonGrammar = longitude.ToString("G", CultureInfo.InvariantCulture);

            using (HttpResponseMessage response = await httpClient.GetAsync($"https://api.met.no/weatherapi/locationforecast/2.0/compact.json?lat={latGrammar}&lon={lonGrammar}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    WeatherForecast weatherForecast = await response.Content.ReadAsAsync<WeatherForecast>();

                    WeatherPoint weatherPoint = new WeatherPoint();

                    weatherPoint.lat = latitude;
                    weatherPoint.lon = longitude;

                    var time = weatherForecast.properties.timeseries[0].time;
                    var weather = weatherForecast.properties.timeseries[0].data.instant.details;

                    weatherPoint.time = time;
                    weatherPoint.air_pressure_at_sea_level = weather.air_pressure_at_sea_level;
                    weatherPoint.air_temperature = weather.air_temperature;
                    weatherPoint.cloud_area_fraction = weather.cloud_area_fraction;
                    weatherPoint.relative_humidity = weather.relative_humidity;
                    weatherPoint.wind_from_direction = weather.wind_from_direction;
                    weatherPoint.wind_speed = weather.wind_speed;

                    weatherPoint.precipitation_amount = weatherForecast.properties.timeseries[0].data.next_1_hours.details.precipitation_amount;

                    return weatherPoint;
                }
                else
                {
                    Console.WriteLine($"{timestamp} getting weatherapi error response: {response.StatusCode}");
                    log.LogInformation($"{timestamp} getting weatherapi error response: {response.StatusCode}");
                }
            }
            return null;
        }
    }
}
