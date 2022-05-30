using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DigitalTvillingSykkel
{
    /// <summary>
    /// Class that handles the API for bicycle information
    /// </summary>
    public static class ApiSykkel
    {
        /// <summary>Gets or sets the Httpclient. <see cref="HttpClient"/></summary>
        /// <value>HttpClient instance</value>
        public static HttpClient Client { get; set; }

        /// <summary>The azure URL </summary>
        /// <value> string with URL received from Azure funksjon app -> Configuration -> Application settings </value>
        public static readonly string azureUrl = Environment.GetEnvironmentVariable("Azure-Twins-Url");

        /// <summary>Initalizes the Sykkel API.</summary>
        /// <value> Client that is configured with headers to get permission to do a GET-request for the Oslo bysykkel API</value>

        public static void InitalizeSykkelApi()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("Client-Identifier", "DigitalTvilling - hiof.no - sykkel data oppdateres hver time - mohammed@hiof.no - bachelorprosjekt");

        }
       

    }
}
