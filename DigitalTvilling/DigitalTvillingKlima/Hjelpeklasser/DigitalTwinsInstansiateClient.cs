﻿using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using System;
using System.Net.Http;

public static class DigitalTwinsInstansiateClient
{
	public static DigitalTwinsClient DigitalTwinsClient(Uri digitalTwinsUrl, HttpClient httpClientInstance)
    {
        try
        {
            var options = new DefaultAzureCredentialOptions { ExcludeVisualStudioCredential = true };
            var cred = new DefaultAzureCredential(options);

            return new DigitalTwinsClient(digitalTwinsUrl, cred,
                new DigitalTwinsClientOptions
                {
                    Transport = new HttpClientTransport(httpClientInstance)
                }
                );
        } catch (Exception e)
        {
            Console.WriteLine("Error detected " + e);
        }

        return null;
       
    }
}