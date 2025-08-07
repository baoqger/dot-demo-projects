using Newtonsoft.Json.Linq;
using PrepareCementingPressureData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

class Program
{
    static void Main()
    {
        string json = @"{
            ""AnnulusDepthBasedResults"": {
                ""2025-07-07T08:28:46.000Z"": [
                    {
                        ""MD"": 0,
                        ""IsInsidePipe"": true,
                        ""Pressure"": 4321.67,
                        ""Temperature"": 115.4,
                        ""ECD"": 1.12,
                        ""ESD"": 0.78,
                        ""PorePressureEFD"": 3400.2,
                        ""FracturePressureEFD"": 4900.6,
                        ""DepthUnit"": ""meters"",
                        ""PressureUnit"": ""psi"",
                        ""TemperatureUnit"": ""Celsius"",
                        ""DensityUnit"": ""ppg""
                    },
                    {
                        ""MD"": 10,
                        ""IsInsidePipe"": false,
                        ""Pressure"": 4587.23,
                        ""Temperature"": 123.9,
                        ""ECD"": 1.18,
                        ""ESD"": 0.91,
                        ""PorePressureEFD"": 3551.5,
                        ""FracturePressureEFD"": 5055.1,
                        ""DepthUnit"": ""meters"",
                        ""PressureUnit"": ""psi"",
                        ""TemperatureUnit"": ""Celsius"",
                        ""DensityUnit"": ""ppg""
                    },
                    {
                        ""MD"": 20,
                        ""IsInsidePipe"": true,
                        ""Pressure"": 4400.12,
                        ""Temperature"": 119.7,
                        ""ECD"": 1.15,
                        ""ESD"": 0.85,
                        ""PorePressureEFD"": 3478.9,
                        ""FracturePressureEFD"": 4987.7,
                        ""DepthUnit"": ""meters"",
                        ""PressureUnit"": ""psi"",
                        ""TemperatureUnit"": ""Celsius"",
                        ""DensityUnit"": ""ppg""
                    },
                    {
                        ""MD"": 30,
                        ""IsInsidePipe"": false,
                        ""Pressure"": 4660.54,
                        ""Temperature"": 127.3,
                        ""ECD"": 1.22,
                        ""ESD"": 0.93,
                        ""PorePressureEFD"": 3599.4,
                        ""FracturePressureEFD"": 5102.3,
                        ""DepthUnit"": ""meters"",
                        ""PressureUnit"": ""psi"",
                        ""TemperatureUnit"": ""Celsius"",
                        ""DensityUnit"": ""ppg""
                    }
                ]
            }
        }";

        string inputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\convertingtestdata.json";
        var inputcontent = File.ReadAllText(inputFilePath);
        string outputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\convertingtestdata-depth-sorted.json";

        // Parse JSON into JObject
        var root = JObject.Parse(inputcontent);

        var result = new Dictionary<string, Dictionary<DateTimeOffset, object>>();

        if (root.TryGetValue("AnnulusDepthBasedResults", out JToken annulusResultsToken) && annulusResultsToken is JObject annulusResults)
        {
            var innerDict = new Dictionary<DateTimeOffset, object>();

            foreach (var dateProperty in annulusResults.Properties())
            {
                if (DateTimeOffset.TryParse(dateProperty.Name, out DateTimeOffset dateTime))
                {
                    // Deserialize the value into CementingPressureProfile
                    var profile = dateProperty.Value.ToObject<CementingPressureProfile>();
                    innerDict[dateTime] = profile;
                }
            }

            result["AnnulusDepthBasedResults"] = innerDict;
        }

        // Example: Accessing data
        // PrintCementingPressureProfile(result);

        var watch = Stopwatch.StartNew();
        var depthSeriesData = DepthSeriesConverter.ConvertTimeSeriesToDepthSeriesStronglyTyped(result);
        var processTime = watch.ElapsedMilliseconds;
        Console.WriteLine($"Processing time: {processTime} ms");

        //foreach (var outerKvp in depthSeriesData)
        //{
        //    Console.WriteLine($"Key: {outerKvp.Key}");
        //    foreach (var depthKvp in outerKvp.Value)
        //    {
        //        Console.WriteLine($"Depth (MD): {depthKvp.Key}, Count: {depthKvp.Value.Count}");
        //        foreach (var resultWithTime in depthKvp.Value)
        //        {
        //            Console.WriteLine($"  Pressure: {resultWithTime.Pressure}, TimeIndex: {resultWithTime.TimeIndex}");
        //        }
        //    }
        //}

        var depthJSON = JsonSerializer.Serialize(depthSeriesData, new JsonSerializerOptions
        {
            WriteIndented = true,
            // PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        File.WriteAllText(outputFilePath, depthJSON);
    }

    static void PrintCementingPressureProfile(Dictionary<string, Dictionary<DateTimeOffset, dynamic>> timeSeriesData)
    {
        foreach (var kvp in timeSeriesData["AnnulusDepthBasedResults"])
        {
            Console.WriteLine($"Date: {kvp.Key}");

            var array = (CementingPressureProfile)kvp.Value;
            foreach (var item in array)
            {
                Console.WriteLine($"  MD: {item.MD}");
            }
        }
    }

    static void GenerateTestDataSet() {
        var dataset = DataSetGenerator.GenerateDataset();
        string outputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\cementingpressuretestdata-compressed.json";
        var depthJSON = JsonSerializer.Serialize(dataset, new JsonSerializerOptions
        {
            WriteIndented = false,
            // PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        File.WriteAllText(outputFilePath, depthJSON);
    }
}
