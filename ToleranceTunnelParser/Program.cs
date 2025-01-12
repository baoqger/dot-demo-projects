using System.IO;
using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Xml;

namespace ToleranceTunnelParser
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileName = @"Data/Input/ToleranceTunnel.json";
            var str = File.ReadAllText(fileName);
            Console.WriteLine("debug xxxx" + str);
            var ttddp = JsonConvert.DeserializeObject<TorleranceTunnelDDP>(str);
            var tt = ttddp.ToleranceTunnel;
            var ttstr = JsonConvert.SerializeObject(tt);
            Console.WriteLine("dedbug: " + ttstr);

        }

        public static T ParseConfigurationFromFile<T>(string xmlFileName) {
            using (var streamReader = File.OpenRead(xmlFileName))
            { 
                var xmlSerializer = new DataContractSerializer(typeof(T)); 
                var config = (T)xmlSerializer.ReadObject(streamReader);
                return config;
            }
        }
    }
}
