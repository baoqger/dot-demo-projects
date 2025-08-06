using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

class Program
{
    const double DepthThreshold = 5.0;

    static void Main()
    {
        string inputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\pressurewindow-decimation.json";
        var inputcontent = File.ReadAllText(inputFilePath);

        // Parse JSON document
        JsonNode root = JsonNode.Parse(inputcontent);

        // Navigate to wellBalance.ECD_RT_DEPTH node
        var ecdNode = root?["wellBalance.ECD_RT_DEPTH"]?.AsObject();

        if (ecdNode == null)
        {
            Console.WriteLine("Data not found");
            return;
        }

        // Count the number of timestamp keys
        int timeSeriesCount = ecdNode.Count;

        Console.WriteLine($"Number of time series data points: {timeSeriesCount}");

        string lastKey = null;
        foreach (var kvp in ecdNode)
        {
            lastKey = kvp.Key;
        }

        // Get profilePoints array from last timestamp node
        var profilePoints = ecdNode[lastKey]?["profilePoints"]?.AsArray();

        if (profilePoints == null)
        {
            Console.WriteLine("profilePoints not found for last timestamp.");
            return;
        }

        // Count profilePoints length
        int length = profilePoints.Count;

        Console.WriteLine($"Last timestamp: {lastKey}");
        Console.WriteLine($"Number of profilePoints: {length}");
    }
}
