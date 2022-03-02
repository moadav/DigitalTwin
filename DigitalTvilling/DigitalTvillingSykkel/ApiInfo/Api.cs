using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DigitalTvillingKlima
{
    public static class Api
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
            Client.DefaultRequestHeaders.Add("User-Agent", "C# bachelor App that update weather info each hour");

        }
       

    }
}
