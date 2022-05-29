using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

/// <summary>Class that handles the creation of DTDL - models and sends them to Azure Digital Twins plattform</summary>
public static class JsonToModel
{

    /// <summary>Creates the model asynchronous.</summary>
    /// <param name="DigitalTwinsClient">The digital twins client.</param>
    /// <param name="FileUrl">The file URL of the DTDL - Model file.</param>
    private static async void CreateModelAsync(DigitalTwinsClient DigitalTwinsClient, String FileUrl)
    {
        ///<summary>gets the DTDL-model files as a List <see cref="List{T}"/></summary>
        List<string> Models = ReturnModel(FileUrl);

        try
        {
            ///<summary>Creates and sends the models to the Azure Digital Twin plattform</summary>
            await DigitalTwinsClient.CreateModelsAsync(Models);
        }
        catch (RequestFailedException e)
        {
            Console.WriteLine($"Upload model error: {e.Status}: {e.Message}");
        }
    }


    /// <summary>Gets the filepath and creates the models.</summary>
    /// <param name="DigitalTwinsClient">The DigitalTwinsClient.</param>
    public static void InitializeModels(DigitalTwinsClient DigitalTwinsClient)
    {
        try
        {

            ///<summary>The filepath to the folder containting the DTDL - models</summary>
            DirectoryInfo modeldirectory = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Modeler"));
            FileInfo[] Files = modeldirectory.GetFiles("*.json");

            ///<summary>Gets each file and creates a Model <see cref="CreateModelAsync(DigitalTwinsClient, string)"/></summary>
            foreach (FileInfo file in Files)
               CreateModelAsync(DigitalTwinsClient, file.FullName);

            
        }
        catch (Exception e)
        {
            Console.WriteLine("An exception occured: " + e);
        }
    }

    /// <summary>Returns the file as a list of string</summary>
    /// <param name="fileUrl">The file URL.</param>
    /// <returns>
    ///   <para>The DTDL - modell as a list of string</para>
    /// </returns>
    private static List<string> ReturnModel(String fileUrl)
    {
        try
        {
            string dtdl = File.ReadAllText(fileUrl);
            var models = new List<string> { dtdl };
            return models;
        }
        catch (Exception e)
        {
            Console.WriteLine("An exception occured: " + e);
            return null;
        }
    }
}
