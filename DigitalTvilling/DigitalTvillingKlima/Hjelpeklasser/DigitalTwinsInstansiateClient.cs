using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using DigitalTvillingKlima;
using System;
using System.Net.Http;

/// <summary>Class that handles the Client for the Azure Digital Twins plattform</summary>
public static class DigitalTwinsInstansiateClient
{


    /// <summary>The Digital Twin Client</summary>
    /// <returns>Intansiates a Class of DigitalTwinsClient</returns>
    public static DigitalTwinsClient DigitalTwinsClient()
    {
        try
        {
            

            ///<value> Configure the Azure Credential to exclude visualstudiocredential to avoid authentication error Azure Digital Twins plattform </value>
            var options = new DefaultAzureCredentialOptions { ExcludeVisualStudioCredential = true };
            ///<value> The DefaultAzureCredential that takes in a DefaultAzureCredentialOptions </value>
            var cred = new DefaultAzureCredential(options);

            ///<value> Returns the DigitalTwinsClient with DefaultAzureCredential and azure URL and (optional) a HttpClientTransport</value>
            return new DigitalTwinsClient(new Uri(Api.AzureUrl), cred,
                new DigitalTwinsClientOptions
                {
                    Transport = new HttpClientTransport(new HttpClient())
                }
                );
        }
        catch (Exception e)
        {
            Console.WriteLine("Error detected on client instansiation " + e);
        }

        return null;

    }
}
