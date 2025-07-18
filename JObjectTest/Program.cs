using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Slb.Cementing.Contracts.V1.DigitalCementPlan;
using System.IO;
// See https://aka.ms/new-console-template for more information
class Program
{


    static void Main(string[] args)
    {

        Console.WriteLine(Guid.NewGuid().ToString());
        return;
        
        string inputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\DigitalCementPlan.json";
        var inputcontent = File.ReadAllText(inputFilePath);
        // var dcp = JsonConvert.DeserializeObject<DigitalCementPlanDto>(inputcontent);
        // JsonConvert.SerializeObject(dcp);
        JObject obj = JObject.Parse(inputcontent);

        var id = Guid.NewGuid().ToString();
        Console.WriteLine(obj["id"]);
        obj["id"] = id;
        var str = obj.ToString();
        var str2 = JsonConvert.SerializeObject(obj);
        Console.WriteLine("hello world");
    }
}
