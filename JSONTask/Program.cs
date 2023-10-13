using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using Slb.Prism.Shared.Contract.ComputationEngine.DataModel.ContextModel;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Reflection;

namespace JSONTask
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            BhaRunModel b = new BhaRunModel();

            string str3 = "{\"frictionFactorPairs\":[{\"casingHole\":0.1,\"openHole\":0.1},{\"casingHole\":0.2,\"openHole\":0.2}, {\"casingHole\":0.2,\"openHole\":0.3}]}";


            string str = "{\"name\": \"toyota\", \"size\": \"small\"}";
            string str2 = "{\"name\": \"chris\", \"age\": 37}";
            string str5 = "{\"left\": 100 , \"right\": 100, \"above\": 100 , \"below\": 100}";

            var dic = new Dictionary<string, Type> {
                { "car", typeof(Car) },
                { "ff", typeof(TnDFrictionFactor)}
            };
            var type = "ff";
            dynamic a = JsonConvert.DeserializeObject(str3, dic[type]);
            //if (a is TnDFrictionFactor model) {
            //    Console.WriteLine(model.FrictionFactorPairs[0].CasingHole);
            //}
            Console.WriteLine(a.FrictionFactorPairs[0].CasingHole);
            var ab = DateTime.Now;
        }
    }

    public class TnDFrictionFactor {
        public FrictionFactorDataPoint[] FrictionFactorPairs { get; set; }
    }

    public class FrictionFactorDataPoint
    {
        public double CasingHole { get; set; }
        public double OpenHole { get; set; }
    }

    public class TrajectoryDeviation {
        public double left { get; set; }
        public double right { get; set; }
        public double above { get; set; }
        public double below { get; set; }
    }

    public class Car { 
        public string name { get; set; }
        public string size { get; set; }
    }

    public class Person { 
        public int age { get; set; }
        public string? name { get; set; }
    }
}