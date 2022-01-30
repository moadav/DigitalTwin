using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.IO;

public static class JsonToModel
{

    public static async void CreateModel(DigitalTwinsClient digitalTwinsClient, String fileUrl)
    {

        List<string> models = ReturnModel(fileUrl);

        try
        {
            await digitalTwinsClient.CreateModelsAsync(models);
        }
        catch (RequestFailedException e)
        {
            Console.WriteLine($"Upload model error: {e.Status}: {e.Message}");
        }
    }

    private static List<string> ReturnModel(String fileUrl)
    {
        string dtdl = File.ReadAllText(fileUrl);
        var models = new List<string> { dtdl };
        return models;
    }




}
