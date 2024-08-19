using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LibA;
using Newtonsoft.Json;

namespace ConsoleTest
{
    public class RawChannelData {
        public DateTimeOffset TimeIndex { get; set; }
        public double Value { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var channelsData = new Dictionary<string, Dictionary<DateTimeOffset, object>>();
            var now = DateTimeOffset.UtcNow;
            var mobDic = new Dictionary<DateTimeOffset, object>() {
                { now, 0.1},
                { now.AddSeconds(1), 0.2},
                { now.AddSeconds(2), 0.3},
                { now.AddSeconds(3), 0.3}
            };
            channelsData.Add("mob", mobDic);

            var ropDic = new Dictionary<DateTimeOffset, object>() {
                { now, 0.1},
                { now.AddSeconds(3), 0.3},
                { now.AddSeconds(1), 0.2},
                { now.AddSeconds(2), 0.0}
                
            };
            channelsData.Add("rop", ropDic);

            var rpmDic = new Dictionary<DateTimeOffset, object>();
            channelsData.Add("rpm", rpmDic);

            

            var lastData = new Dictionary<string, KeyValuePair<DateTimeOffset, object>>();
            
            var lastROP = new KeyValuePair<DateTimeOffset, object>(now.AddSeconds(-1), 0.8);
            lastData.Add("rop", lastROP);

            var lastMOB = new KeyValuePair<DateTimeOffset, object>(now.AddSeconds(-1), 0.8);
            lastData.Add("mob", lastMOB);

            Console.WriteLine(JsonConvert.SerializeObject(channelsData));
            MergeData(channelsData, lastData);
            Console.WriteLine(JsonConvert.SerializeObject(channelsData));
            Console.ReadLine();

            return;
            // Create the outer dictionary
            IDictionary<string, IDictionary<DateTimeOffset, object>> outerDictionary = new Dictionary<string, IDictionary<DateTimeOffset, object>>();

            // Populate the outer dictionary with 5 keys
            for (int i = 1; i <= 5; i++)
            {
                // Create an inner dictionary for each key
                IDictionary<DateTimeOffset, object> innerDictionary = new Dictionary<DateTimeOffset, object>();

                // Populate the inner dictionary with 50 keys and random double values
                for (int j = 1; j <= 50; j++)
                {
                    // innerDictionary.Add(DateTimeOffset.Now.AddMinutes(j), GetRandomDouble());
                }

                // Add the inner dictionary to the outer dictionary
                outerDictionary.Add("Key" + i, innerDictionary);
            }

            // Display the populated outer dictionary
            foreach (var outerEntry in outerDictionary)
            {
                Console.WriteLine($"Outer Key: {outerEntry.Key}");
                foreach (var innerEntry in outerEntry.Value)
                {
                    Console.WriteLine($"   Inner Key: {innerEntry.Key}, Value: {innerEntry.Value}");
                }
            }
            Console.ReadLine();

            // Create a new dictionary to store the averages using LINQ
            IDictionary<string, double> averagesDictionary = outerDictionary.ToDictionary(
                outerEntry => outerEntry.Key,
                outerEntry => outerEntry.Value.Values.OfType<double>().DefaultIfEmpty(-999.25).Average()
            );

            foreach (var entry in averagesDictionary)
            {
                Console.WriteLine($"Key: {entry.Key}, Average Value: {entry.Value}");
            }


            Console.ReadLine();

            Console.WriteLine(averagesDictionary.ContainsKey("Key1"));

            Console.WriteLine(averagesDictionary.ContainsKey("Key6"));
            Console.ReadLine();
        }

        static void MergeData(Dictionary<string, Dictionary<DateTimeOffset, object>> channelsData, Dictionary<string, KeyValuePair<DateTimeOffset, object>> lastData) {
            foreach (var ele in lastData)
            {
                if (channelsData.ContainsKey(ele.Key))
                {
                    channelsData[ele.Key].Add(ele.Value.Key, ele.Value.Value);
                }
                else
                {
                    channelsData.Add(ele.Key, new Dictionary<DateTimeOffset, object>() { { ele.Value.Key, ele.Value.Value } });
                }
            }
        }


        static void GetMultiValue(Dictionary<string, Dictionary<DateTimeOffset, object>> channelsData, string channelName, out double? start, out double? end, out double? course) {
            if (channelsData.ContainsKey(channelName))
            {
                var arr = channelsData[channelName]?.Values?.OfType<double>();
                start = arr.Min();
                end = arr.Max();
                course = end - start;
            }
            else
            {
                start = end = course = null;
            }
        }
        static double GetRandomDouble()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.NextDouble() * 100; // Generate random value between 0 and 100
        }

        static void TestAnyDic() {
            Dictionary<string, int> data = new Dictionary<string, int>();  // Create an empty dictionary

            // Directly check if 'data' is not null
            if (data != null)
            {
                Console.WriteLine("The 'data' dictionary is not null");
            }
            else
            {
                Console.WriteLine("The 'data' dictionary is null");
            }

            // Check if 'data' contains any elements using Any() method
            if (data.Any())  // Here we are using the Any() method to check if the dictionary contains any key-value pairs
            {
                Console.WriteLine("The 'data' dictionary contains elements");
            }
            else
            {
                Console.WriteLine("The 'data' dictionary is empty");
            }
        }
    }
}
