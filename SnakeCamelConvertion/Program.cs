using System;
using Newtonsoft.Json.Linq;
using System.Globalization;

class Program
{
    static void Main()
    {
        string inputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\pumpschedule.json";
        string outputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\pumpschedule_camel.json";
        var inputcontent = File.ReadAllText(inputFilePath);
        string snakeCaseJson = @"{
            ""first_name"": ""Alice"",
            ""age"": 28,
            ""home_address"": {
                ""street_name"": ""Main St"",
                ""zip_code"": ""12345""
            }
        }";

        JObject obj = JObject.Parse(inputcontent);
        JObject camelCaseObj = ConvertKeysToCamelCase(obj);
        var camelString = camelCaseObj.ToString();
        // Write the converted JSON to the output file
        Console.WriteLine(camelString);
        // Write the converted JSON to the output file
        File.WriteAllText(outputFilePath, camelString);
    }

    static JObject ConvertKeysToCamelCase(JObject original)
    {
        JObject newObj = new JObject();

        foreach (var property in original.Properties())
        {
            string camelCaseKey = SnakeToCamel(property.Name);

            if (property.Value.Type == JTokenType.Object)
            {
                // Recursively convert nested objects
                newObj[camelCaseKey] = ConvertKeysToCamelCase((JObject)property.Value);
            }
            else if (property.Value.Type == JTokenType.Array)
            {
                // Handle arrays, converting objects inside the array
                JArray array = new JArray();
                foreach (var item in property.Value)
                {
                    if (item.Type == JTokenType.Object)
                    {
                        array.Add(ConvertKeysToCamelCase((JObject)item));
                    }
                    else
                    {
                        array.Add(item);
                    }
                }
                newObj[camelCaseKey] = array;
            }
            else
            {
                newObj[camelCaseKey] = property.Value;
            }
        }

        return newObj;
    }

    static string SnakeToCamel(string snake)
    {
        var parts = snake.Split('_');
        if (parts.Length == 1)
            return parts[0];

        for (int i = 1; i < parts.Length; i++)
        {
            if (parts[i].Length > 0)
                parts[i] = char.ToUpper(parts[i][0], CultureInfo.InvariantCulture) + parts[i].Substring(1);
        }
        return string.Join("", parts);
    }
}