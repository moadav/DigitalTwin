using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using System;
using System.Net.Http;

public static class DigitalTwinsInstansiateClient
{
    public static DigitalTwinsClient DigitalTwinsClient(Uri digitalTwinsUrl)
    {
        try
        {
            var options = new DefaultAzureCredentialOptions { ExcludeVisualStudioCredential = true };
            var cred = new DefaultAzureCredential(options);
            //var cred = new ManagedIdentityCredential();
            return new DigitalTwinsClient(digitalTwinsUrl, cred,
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
