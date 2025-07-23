using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        string inputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\pressurewindow-decimation.json";
        var inputcontent = File.ReadAllText(inputFilePath);
        string outputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\depthbasedseries-decimation-fixed.json";

        var doc = JsonDocument.Parse(inputcontent);
        var root = doc.RootElement;
        var series = root.GetProperty("wellBalance.ECD_RT_DEPTH");

        var watch = Stopwatch.StartNew();

        var depthSeries = new ConcurrentDictionary<double, JsonArray>();

        Parallel.ForEach(series.EnumerateObject(), timeEntry =>
        {
            string timestamp = timeEntry.Name;
            var profilePoints = timeEntry.Value.GetProperty("profilePoints");

            foreach (var point in profilePoints.EnumerateArray())
            {
                if (point.TryGetProperty("md", out var mdProp) && mdProp.TryGetDouble(out double md))
                {
                    // Get or create JsonArray for this depth
                    var jsonArray = depthSeries.GetOrAdd(md, _ => new JsonArray());

                    var newObj = new JsonObject();

                    foreach (var prop in point.EnumerateObject())
                    {
                        if (prop.Name != "md" && prop.Name != "tvd")
                        {
                            newObj[prop.Name] = JsonNode.Parse(prop.Value.GetRawText());
                        }
                    }

                    newObj["timeIndex"] = timestamp;

                    // Lock on the JsonArray instance to append safely
                    lock (jsonArray)
                    {
                        jsonArray.Add(newObj);
                    }
                }
            }
        });

        foreach (var kvp in depthSeries)
        {
            var jsonArray = kvp.Value;

            var sortedNodes = jsonArray
                .OrderBy(node => node?["timeIndex"]?.GetValue<string>())
                .ToArray();

            jsonArray.Clear();

            foreach (var node in sortedNodes)
            {
                jsonArray.Add(node);
            }
        }

        var processTime = watch.ElapsedMilliseconds;
        Console.WriteLine($"Processing time: {processTime} ms");

        // Serialize final output ordered by depth ascending
        var outputObj = new JsonObject();
        foreach (var kvp in depthSeries.OrderBy(k => k.Key))
        {
            outputObj[kvp.Key.ToString("F2")] = kvp.Value;
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        string finalJson = outputObj.ToJsonString(options);

        File.WriteAllText(outputFilePath, finalJson);
    }
}