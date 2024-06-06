using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace PrepareSlideSheetData
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var inputPath = @"C:\Users\jbao6\Desktop\automaticslide\minify.json";
            var content = File.ReadAllText(inputPath);
            ChannelData channels = JsonConvert.DeserializeObject<ChannelData>(content);
            // Console.WriteLine(channels?.Log[0].Data[0].Value);
            var holeDepths = channels?.Log[0];

            Console.ReadLine();

            var mocks = GenerateChannelLogs();
            mocks.Log.Add(holeDepths);
            var outputPath = @"C:\Users\jbao6\Desktop\automaticslide\mock.json";
            File.WriteAllText(outputPath, JsonConvert.SerializeObject(mocks, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

        public static ChannelData GenerateChannelLogs() { 
            var channels = new ChannelData();
            List<ChannelLog> logs = new List<ChannelLog>() { 
                new ChannelLog { 
                    InputKey = "WOB",
                    Uri = "test",
                    Mnemonic = "test",
                    Unit = "test",
                    Quantity = "test",
                    Shape = "test",
                    ValueType = "double",
                    Data = GenerateChannelDataPoints(),
                    Derivation = "raw"
                },
                new ChannelLog {
                    InputKey = "ROP",
                    Uri = "test",
                    Mnemonic = "test",
                    Unit = "test",
                    Quantity = "test",
                    Shape = "test",
                    ValueType = "double",
                    Data = GenerateChannelDataPoints(),
                    Derivation = "raw"
                }
            };
            channels.Log = logs;
            channels.ApplyDaylightSaving = true;
            return channels;
        }

        public static List<ChannelDataPoint> GenerateChannelDataPoints() { 
            List<ChannelDataPoint> data = new List<ChannelDataPoint>();

            var startTime = DateTimeOffset.ParseExact("2022-05-03T16:59:59+00:00", "yyyy-MM-dd'T'HH:mm:ss+00:00", null);
            var endTime = DateTimeOffset.ParseExact("2022-05-03T20:00:00+00:00", "yyyy-MM-dd'T'HH:mm:ss+00:00", null);

            for (DateTimeOffset timeIndex = startTime; timeIndex < endTime;)
            {

                var point = new ChannelDataPoint()
                {
                    Index = timeIndex.ToString("yyyy-MM-dd'T'HH:mm:ss+00:00"),
                    Value = GetRandomDouble()
                };
                data.Add(point);
                // Console.WriteLine("debug index: " + timeIndex.ToString("yyyy-MM-dd'T'HH:mm:ss+00:00") + "value: " + GetRandomDouble());
                timeIndex = timeIndex.Add(TimeSpan.FromSeconds(1));
            }
            return data;
        }

        public static double GetRandomDouble()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return Math.Round(random.NextDouble() * 100, 2); // Generate random value between 0 and 100
        }
    }

    public class ChannelDataPoint {
        public string Index { get; set; }

        public double Value { get; set; }
    }

    public class ChannelLog {
        public string InputKey { get; set; }
        public string Uri { get; set; }
        public string Mnemonic { get; set; }
        public string Unit { get; set; }
        public string Quantity { get; set; }
        public string Shape { get; set; }
        public string ValueType { get; set; }
        public List<ChannelDataPoint> Data { get; set; }
        public string Derivation { get; set; } 

    }

    public class ChannelData {
        public List<ChannelLog> Log { get; set; }
        public bool ApplyDaylightSaving { get; set; }
    }
        
    
}
