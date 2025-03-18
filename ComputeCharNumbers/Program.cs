using Newtonsoft.Json.Linq;

namespace ComputeCharNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Load the JSON file
            string jsonFilePath = "C:\\develop\\study\\test\\nam-well1.json";
            string jsonData = File.ReadAllText(jsonFilePath);

            // Parse the JSON data to JToken
            JToken jsonToken = JToken.Parse(jsonData);

            // Calculate the average string length at the outermost object level
            double averageStringLength = CalculateAverageStringLength(jsonToken);

            Console.WriteLine($"The average string length at the outermost object level is: {averageStringLength}");
        }

        static double CalculateAverageStringLength(JToken token)
        {
            // Ensure token is an array or starts with an array of objects
            if (token.Type != JTokenType.Array)
            {
                throw new ArgumentException("Expected a JSON array at the root.");
            }

            // Initialize total length and count of strings
            int totalLength = 0;
            int objectCount = 0;

            // Iterate through each object in the outermost array
            foreach (var outerObject in token.Children<JObject>())
            {
                // Get all string lengths from the outermost object
                int[] lengths = GetStringLengths(outerObject);

                totalLength += lengths.Sum();
                objectCount += 1;
            }
            Console.WriteLine("total length: " + totalLength);
            Console.WriteLine("total word number: " + objectCount);
            // Calculate the average string length
            return objectCount > 0 ? (double)totalLength / objectCount : 0.0;
        }

        // Method to gather string lengths from a JObject
        static int[] GetStringLengths(JObject jObject)
        {
            var lengths = new List<int>();

            // Recursive method to traverse the JObject
            void Traverse(JToken t)
            {
                if (t.Type == JTokenType.Object)
                {
                    foreach (var property in t.Children<JProperty>())
                    {
                        Traverse(property.Value);
                    }
                }
                else if (t.Type == JTokenType.Array)
                {
                    foreach (var item in t.Children())
                    {
                        Traverse(item);
                    }
                }
                else if (t.Type == JTokenType.String)
                {
                    lengths.Add(t.ToString().Length);
                }
            }

            // Start traversal
            Traverse(jObject);

            return lengths.ToArray();
        }
    }
}
