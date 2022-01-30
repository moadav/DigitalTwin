using System.Net.Http;
using System.Net.Http.Headers;

namespace DigitalTvillingKlima
{
    public static class Api
    {
        public static HttpClient Client { get; set; } 


        public static void initalizeApi()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("User-Agent", "C# App, digital twins which collects weather data each hour");

        }
       

    }
}
