using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DigitalTvillingSykkel
{
    public static class ApiSykkel
    {
        public static HttpClient Client { get; set; }
        public static readonly string azureUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL");
        public static readonly HttpClient singletonHttpClientInstance = new HttpClient();

        public static void InitalizeSykkelApi()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("Client-Identifier", "DigitalTvilling - hiof.no - sykkel data oppdateres hver time - mohammedalidavami@gmail.com - bachelorprosjekt");

        }
       

    }
}
