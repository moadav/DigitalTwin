using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public static class JsonToModel
{

    private static async void CreateModel(DigitalTwinsClient digitalTwinsClient, String fileUrl)
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


    public static void InitializeModels(DigitalTwinsClient digitalTwinsClient)
    {
        
        DirectoryInfo modeldirectory = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Modeler"));
        FileInfo[] Files = modeldirectory.GetFiles("*.json"); 
      

        foreach (FileInfo file in Files)
        {
            JsonToModel.CreateModel(digitalTwinsClient, file.FullName);


        }


    }

    private static List<string> ReturnModel(String fileUrl)
    {
        string dtdl = File.ReadAllText(fileUrl);
        var models = new List<string> { dtdl };
        return models;
    }




}
