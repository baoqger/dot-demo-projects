using System;
using System.Security.AccessControl;
using System.Threading;
using TaskCancellation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


public class StreamingRange
{
    public DateTimeOffset StartTime { get; set; }
    /// <summary>
    /// The replay time range covered in an iot message.
    /// </summary>
    [JsonProperty("ReplaySpeed")]
    public int SecondsPerMessage { get; set; }

    [JsonIgnore]
    public double SecondsBetweenMessages { get; set; }
}

public class Tunnel {
    public string Id { get; set; }
    public long LastUpdateTime { get; set; }
    public double? Value { get; set; }
}

public static class RiskConvert {
    public static async Task<string> GetCombinedRiskContent(List<string> ids) {
        var jsonTasks = ids.Select(id =>
        {
            var res = RiskService.GetRiskAsync(id);
            Console.WriteLine("convert ...: " + id);
            return res;
        }).ToList();

        var jsonModels = await Task.WhenAll(jsonTasks);
        return JsonConvert.SerializeObject(jsonModels);
    }
}

public static class RiskService {
    public static async Task<string> GetRiskAsync(string id) { 
        await Task.Delay(5000);
        Console.WriteLine("service ...: " + id);
        return id;
    }
}

public class Example
{
    public static void Main()
    {
        var now = DateTimeOffset.UtcNow;
        var settings = new List<StreamingRange> {
            new StreamingRange {
                StartTime = now,
                SecondsPerMessage = 1
            },
            new StreamingRange {
                StartTime = now.AddHours(-5),
                SecondsPerMessage = 2
            },
            new StreamingRange {
                StartTime = now.AddHours(-10),
                SecondsPerMessage = 3
            }
        };

        var s = JsonConvert.SerializeObject(settings ?? new List<StreamingRange>(), new JsonSerializerSettings { 
            DateFormatString = "yyyy-MM-dd'T'HH:mm:ss+00:00",
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });
        

        Guid? nowtime = Guid.NewGuid();

        var a = "test;";
        var abc = $"{a}";

        if (nowtime.HasValue) abc += $"{a}";

        Console.WriteLine("debug abc: " + abc);

        string UnitSystem = "Metric";
        var name = nameof(UnitSystem);
        Console.WriteLine(name);

        Dictionary<string, string> header = new Dictionary<string, string>() {
            { "UnitSystem", "Metric" }
        };

        header.Add(nameof(UnitSystem), "English");
        Console.WriteLine("debug abc: ");
        return;
        // Create the token source.
        CancellationTokenSource cts = new CancellationTokenSource();

        // Pass the token to the cancelable operation.
        ThreadPool.QueueUserWorkItem(new WaitCallback(DoSomeWork), cts.Token);
        Thread.Sleep(2500);

        // Request cancellation.
        cts.Cancel();
        Console.WriteLine("Cancellation set in token source...");
        Thread.Sleep(2500);
        // Cancellation should have happened, so call Dispose.
        cts.Dispose();
    }

    static async Task<int> CallRemoteSerivce() {
        var service = new RemoteService();
        int retry = 0;
        while (retry <= 4) { 
            retry++;
            await Task.Delay(1000 * retry);
            var value = await service.GetIntAsync();
            Console.WriteLine("debug value: " + value);
        }
        return retry;
    }

    // Thread 2: The listener
    static void DoSomeWork(object? obj)
    {
        if (obj is null)
            return;

        CancellationToken token = (CancellationToken)obj;

        for (int i = 0; i < 100000; i++)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("In iteration {0}, cancellation has been requested...",
                                  i + 1);
                // Perform cleanup if necessary.
                //...
                // Terminate the operation.
                break;
            }
            // Simulate some work.
            Thread.SpinWait(500000);
        }
    }
}
// The example displays output like the following:
//       Cancellation set in token source...
//       In iteration 1430, cancellation has been requested... 