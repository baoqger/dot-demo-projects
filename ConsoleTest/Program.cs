using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibA;
using Newtonsoft.Json;

namespace ConsoleTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            string str5 = "{\"left\": 100 , \"right\": 100, \"above\": 100 , \"below\": 100}";
            TrajectoryDeviation t = JsonConvert.DeserializeObject<TrajectoryDeviation>(str5);
            Console.WriteLine(t.Right);
            Console.WriteLine(t.Unit);
            Console.ReadLine();
        }
    }
}
