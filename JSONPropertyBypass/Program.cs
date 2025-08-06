using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JsonPropertyIgnoreDemo
{
    // Sample class with JsonProperty attribute renaming properties
    public class PressureProfile
    {
        [JsonProperty("depth_meters")]
        public double Depth { get; set; }

        [JsonProperty("pressure_psi")]
        public double Pressure { get; set; }

        [JsonProperty("temperature_celsius")]
        public double Temperature { get; set; }
    }

    // Custom ContractResolver that ignores [JsonProperty] attributes
    public class IgnoreJsonPropertyContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, MemberSerialization memberSerialization)
        {
            // Call base but force MemberSerialization.OptOut to ignore attributes like [JsonProperty]
            var prop = base.CreateProperty(member, MemberSerialization.OptOut);

            // Use the actual property name instead of JsonProperty attribute name
            prop.PropertyName = member.Name;

            return prop;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var profile = new PressureProfile
            {
                Depth = 1234.5,
                Pressure = 5678.9,
                Temperature = 98.6
            };

            // 1. Serialize with default settings (attributes honored)
            string jsonWithAttributes = JsonConvert.SerializeObject(profile, Formatting.Indented);
            Console.WriteLine("Serialization with [JsonProperty] attributes:");
            Console.WriteLine(jsonWithAttributes);
            Console.WriteLine();

            // 2. Serialize using custom ContractResolver to ignore [JsonProperty]
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new IgnoreJsonPropertyContractResolver(),
                Formatting = Formatting.Indented
            };

            string jsonIgnoringAttributes = JsonConvert.SerializeObject(profile, settings);
            Console.WriteLine("Serialization ignoring [JsonProperty] attributes:");
            Console.WriteLine(jsonIgnoringAttributes);
        }
    }
}