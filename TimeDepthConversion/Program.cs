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
        string inputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\mockdata-margin.json";
        var inputcontent = File.ReadAllText(inputFilePath);

        var doc = JsonDocument.Parse(inputcontent);
        var root = doc.RootElement;
        var series = root.GetProperty("wellBalance.ECD_RT_DEPTH");

        // count the number of timestamps 

        // Step 1: Collect all depths across timestamps (flattened list)
        var allDepths = new List<double>();
        foreach (var timeEntry in series.EnumerateObject())
        {
            var profilePoints = timeEntry.Value.GetProperty("profilePoints");
            foreach (var point in profilePoints.EnumerateArray())
            {
                double depth = point.GetProperty("depth").GetDouble();
                allDepths.Add(depth);
            }
        }

        // Step 2: Cluster depths based on threshold
        var depthClusters = ClusterDepths(allDepths, DepthThreshold);

        // Step 3: Build depth-based series with clustered depths as keys
        // Dictionary<clusterDepth, Dictionary<timestamp, pressure>>
        var depthSeries = new SortedDictionary<double, Dictionary<string, double>>();
        foreach (var clusterDepth in depthClusters)
        {
            depthSeries[clusterDepth] = new Dictionary<string, double>();
        }

        // Step 4: For each timestamp, assign each depth to cluster centroid and store pressure
        foreach (var timeEntry in series.EnumerateObject())
        {
            string timestamp = timeEntry.Name;
            var profilePoints = timeEntry.Value.GetProperty("profilePoints");

            foreach (var point in profilePoints.EnumerateArray())
            {
                double depth = point.GetProperty("depth").GetDouble();
                double pressure = point.GetProperty("pressure").GetDouble();

                double? clusterDepth = FindClosestCluster(depth, depthClusters, DepthThreshold);
                if (clusterDepth.HasValue)
                {
                    // Assign pressure to clustered depth at this timestamp
                    depthSeries[clusterDepth.Value][timestamp] = pressure;
                }
                else
                {
                    // If no cluster found, optionally handle or skip
                }
            }
        }

        // Step 5: Print depth-based series
        foreach (var depthEntry in depthSeries)
        {
            Console.WriteLine($"Depth Cluster: {depthEntry.Key:F2}");
            foreach (var timePressure in depthEntry.Value.OrderBy(kvp => kvp.Key))
            {
                Console.WriteLine($"  {timePressure.Key}: {timePressure.Value}");
            }
        }
    }

    // Cluster depths into groups where members are within threshold of cluster mean
    static List<double> ClusterDepths(List<double> depths, double threshold)
    {
        if (depths.Count == 0)
            return new List<double>();

        depths.Sort();
        var clusters = new List<List<double>> { new List<double> { depths[0] } };

        for (int i = 1; i < depths.Count; i++)
        {
            double current = depths[i];
            var lastCluster = clusters.Last();
            double lastClusterMean = lastCluster.Average();

            if (Math.Abs(current - lastClusterMean) <= threshold)
            {
                lastCluster.Add(current);
            }
            else
            {
                clusters.Add(new List<double> { current });
            }
        }

        // Return cluster centroids
        return clusters.Select(c => c.Average()).ToList();
    }

    // Find closest cluster centroid within threshold
    static double? FindClosestCluster(double depth, List<double> clusters, double threshold)
    {
        double? closest = null;
        double minDist = double.MaxValue;

        foreach (var clusterDepth in clusters)
        {
            double dist = Math.Abs(depth - clusterDepth);
            if (dist <= threshold && dist < minDist)
            {
                minDist = dist;
                closest = clusterDepth;
            }
        }

        return closest;
    }
}