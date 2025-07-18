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
    const double DepthThreshold = 5.0;

    static void Main()
    {
        string inputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\pressurewindow-decimation.json";
        var inputcontent = File.ReadAllText(inputFilePath);
        string outputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\depthbasedseries-decimation.json";

        var doc = JsonDocument.Parse(inputcontent);
        var root = doc.RootElement;
        var series = root.GetProperty("wellBalance.ECD_RT_DEPTH");

        var watch = Stopwatch.StartNew();

        // Step 1: Collect all md depths
        var allDepths = new List<double>();
        foreach (var timeEntry in series.EnumerateObject())
        {
            var profilePoints = timeEntry.Value.GetProperty("profilePoints");
            foreach (var point in profilePoints.EnumerateArray())
            {
                if (point.TryGetProperty("md", out var mdProp) && mdProp.TryGetDouble(out double md))
                {
                    allDepths.Add(md);
                }
            }
        }

        // Step 2: Cluster md depths (incremental mean)
        var depthClusters = ClusterDepths(allDepths, DepthThreshold);

        // Step 3: Prepare concurrent dictionary for depth-based series
        var depthSeries = new ConcurrentDictionary<double, JsonArray>();
        foreach (var clusterDepth in depthClusters)
        {
            depthSeries[clusterDepth] = new JsonArray();
        }

        // Step 4: Process timestamps and profilePoints in parallel
        Parallel.ForEach(series.EnumerateObject(), timeEntry =>
        {
            string timestamp = timeEntry.Name;
            var profilePoints = timeEntry.Value.GetProperty("profilePoints");

            foreach (var point in profilePoints.EnumerateArray())
            {
                if (point.TryGetProperty("md", out var mdProp) && mdProp.TryGetDouble(out double md))
                {
                    double? clusterDepth = FindClosestCluster(md, depthClusters, DepthThreshold);
                    if (clusterDepth.HasValue)
                    {
                        var newObj = new JsonObject();

                        foreach (var prop in point.EnumerateObject())
                        {
                            if (prop.Name != "md" && prop.Name != "tvd")
                            {
                                newObj[prop.Name] = JsonNode.Parse(prop.Value.GetRawText());
                            }
                        }

                        newObj["timeIndex"] = timestamp;

                        // Thread-safe add
                        lock (depthSeries[clusterDepth.Value])
                        {
                            depthSeries[clusterDepth.Value].Add(newObj);
                        }
                    }
                }
            }
        });

        var processTime = watch.ElapsedMilliseconds;
        Console.WriteLine($"Processing time: {processTime} ms");

        // Step 5: Serialize final output
        var outputObj = new JsonObject();
        foreach (var kvp in depthSeries.OrderBy(k => k.Key))
        {
            outputObj[kvp.Key.ToString("F2")] = kvp.Value;
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        string finalJson = outputObj.ToJsonString(options);

        File.WriteAllText(outputFilePath, finalJson);
    }

    // Cluster class for incremental averaging
    class Cluster
    {
        public double Sum;
        public int Count;
        public List<double> Depths = new List<double>();

        public double Mean => Sum / Count;

        public Cluster(double initialDepth)
        {
            Sum = initialDepth;
            Count = 1;
            Depths.Add(initialDepth);
        }

        public void Add(double depth)
        {
            Sum += depth;
            Count++;
            Depths.Add(depth);
        }
    }

    static List<double> ClusterDepths(List<double> depths, double threshold)
    {
        if (depths.Count == 0)
            return new List<double>();

        depths.Sort();
        var clusters = new List<Cluster> { new Cluster(depths[0]) };

        for (int i = 1; i < depths.Count; i++)
        {
            double current = depths[i];
            var lastCluster = clusters.Last();

            if (Math.Abs(current - lastCluster.Mean) <= threshold)
                lastCluster.Add(current);
            else
                clusters.Add(new Cluster(current));
        }

        return clusters.Select(c => c.Mean).ToList();
    }

    static double? FindClosestCluster(double depth, List<double> sortedClusters, double threshold)
    {
        int idx = sortedClusters.BinarySearch(depth);
        if (idx >= 0)
            return sortedClusters[idx]; // exact match

        idx = ~idx; // insertion point

        double? bestCluster = null;
        double minDist = double.MaxValue;

        if (idx - 1 >= 0)
        {
            double dist = Math.Abs(depth - sortedClusters[idx - 1]);
            if (dist <= threshold && dist < minDist)
            {
                minDist = dist;
                bestCluster = sortedClusters[idx - 1];
            }
        }
        if (idx < sortedClusters.Count)
        {
            double dist = Math.Abs(depth - sortedClusters[idx]);
            if (dist <= threshold && dist < minDist)
            {
                minDist = dist;
                bestCluster = sortedClusters[idx];
            }
        }

        return bestCluster;
    }
}
