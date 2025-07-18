using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Diagnostics;

class Program
{
    const double DepthThreshold = 5.0;

    static void Main()
    {
        string json = @"{
            ""wellBalance.ECD_RT_DEPTH"": {
                ""2025-07-11T15:28:02.000Z"": {
                    ""profilePoints"": [
                        {
                            ""md"": 0.0,
                            ""tvd"": 0.0,
                            ""pressure"": { ""p50"": 101.325 },
                            ""ecd"": { ""p50"": ""NaN"" },
                            ""esdPressure"": { ""p50"": 101.325 },
                            ""esd"": { ""p50"": ""NaN"" }
                        },
                        {
                            ""md"": 10.0,
                            ""tvd"": 9.8,
                            ""pressure"": { ""p50"": 201.325 },
                            ""ecd"": { ""p50"": 10.5 },
                            ""esdPressure"": { ""p50"": 201.325 },
                            ""esd"": { ""p50"": 10.5 }
                        }
                    ]
                },
                ""2025-07-11T15:29:02.000Z"": {
                    ""profilePoints"": [
                        {
                            ""md"": 3.0,
                            ""tvd"": 3.0,
                            ""pressure"": { ""p50"": 99.99 },
                            ""ecd"": { ""p50"": ""NaN"" },
                            ""esdPressure"": { ""p50"": 99.99 },
                            ""esd"": { ""p50"": ""NaN"" }
                        },
                        {
                            ""md"": 10.0,
                            ""tvd"": 9.8,
                            ""pressure"": { ""p50"": 201.325 },
                            ""ecd"": { ""p50"": 10.5 },
                            ""esdPressure"": { ""p50"": 201.325 },
                            ""esd"": { ""p50"": 10.5 }
                        }
                    ]
                }
            }
        }";

        string inputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\pressurewindow-decimation.json";
        var inputcontent = File.ReadAllText(inputFilePath);

        string outputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\depthbasedseries-decimation.json";

        var doc = JsonDocument.Parse(inputcontent);
        var root = doc.RootElement;
        var series = root.GetProperty("wellBalance.ECD_RT_DEPTH");

        var watch = Stopwatch.StartNew();

        // 1. Collect all md depths
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

        // 2. Cluster md depths
        var depthClusters = ClusterDepths(allDepths, DepthThreshold);

        // 3. Build depth-based series:
        // Dictionary<clusterDepth, JsonArray> - each JsonArray contains profilePoints with timeIndex added, md & tvd removed
        var depthSeries = new Dictionary<double, JsonArray>();
        foreach (var clusterDepth in depthClusters)
        {
            depthSeries[clusterDepth] = new JsonArray();
        }

        // 4. For each timestamp, assign each md to cluster centroid and build modified JsonObject
        foreach (var timeEntry in series.EnumerateObject())
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
                        // Create JsonObject excluding "md" and "tvd"
                        var newObj = new JsonObject();

                        foreach (var prop in point.EnumerateObject())
                        {
                            if (prop.Name != "md" && prop.Name != "tvd")
                            {
                                // Copy property as JsonNode
                                newObj[prop.Name] = JsonNode.Parse(prop.Value.GetRawText());
                            }
                        }

                        // Add timeIndex property
                        newObj["timeIndex"] = timestamp;

                        // Add to corresponding cluster depth's array
                        depthSeries[clusterDepth.Value].Add(newObj);
                    }
                    else
                    {
                        // Optionally handle no cluster found
                    }
                }
            }
        }

        var processTime = watch.ElapsedMilliseconds;
        Console.WriteLine($"Processing time: {processTime} ms");

        // 5. Serialize final depthSeries dictionary to JSON string for output
        var options = new JsonSerializerOptions { WriteIndented = true };

        var outputObj = new JsonObject();
        foreach (var kvp in depthSeries.OrderBy(k => k.Key))
        {
            outputObj[kvp.Key.ToString("F2")] = kvp.Value;
        }

        string finalJson = outputObj.ToJsonString(options);
        File.WriteAllText(outputFilePath, finalJson);

        // Console.WriteLine(finalJson);
    }

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

        return clusters.Select(c => c.Average()).ToList();
    }

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