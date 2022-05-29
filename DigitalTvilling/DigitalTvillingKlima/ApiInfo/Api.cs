using System;
using System.Net.Http;
using System.Net.Http.Headers;


namespace DigitalTvillingKlima
{
    /// <summary>
    /// Class that handles the API for Climate information
    /// </summary>
    public static class Api
    {

        /// <summary>Gets or sets the Httpclient. <see cref="HttpClient"/></summary>
        /// <value>HttpClient instance</value>
        public static HttpClient Client { get; set; }

        /// <summary>The azure URL </summary>
        /// <value> string with URL received from Azure funksjon app -> Configuration -> Application settings </value>
        public static readonly string AzureUrl = Environment.GetEnvironmentVariable("Azure-Twins-Url");


        /// <summary>Initalizes the klima API.</summary>
        /// <value> Client that is configured with headers to get permission to do a GET-request for the Meteorologisk institutt API</value>
        public static void InitalizeKlimaApi()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("User-Agent", "hiof.no - bachelorproject updates weather info every hour - digitaltvilling ");
            

        }
       

    }
}
