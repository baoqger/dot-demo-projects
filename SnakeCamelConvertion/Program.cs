using System;
using Newtonsoft.Json.Linq;
using System.Globalization;

class Program
{
    static void Main()
    {
        string inputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\pumpschedule_camel.json";
        string outputFilePath = @"C:\Users\jbao6\Desktop\dev\cementing\pumpschedule_snake.json";
        var inputcontent = File.ReadAllText(inputFilePath);

        JObject obj = JObject.Parse(inputcontent);

        // from snake to camel case
        // JObject camelCaseObj = ConvertKeysToCamelCase(obj);
        // var camelString = camelCaseObj.ToString();
        // Console.WriteLine(camelString);
        // File.WriteAllText(outputFilePath, camelString);

        // from camel to snake case
        JObject snakeCaseObj = ConvertKeysToSnakeCase(obj);
        var snakeString = snakeCaseObj.ToString();
        Console.WriteLine(snakeString);
        File.WriteAllText(outputFilePath, snakeString);
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

    static JObject ConvertKeysToSnakeCase(JObject original)
    {
        JObject newObj = new JObject();

        foreach (var property in original.Properties())
        {
            string snakeCaseKey = CamelToSnake(property.Name);

            if (property.Value.Type == JTokenType.Object)
            {
                // Recursively convert nested objects
                newObj[snakeCaseKey] = ConvertKeysToSnakeCase((JObject)property.Value);
            }
            else if (property.Value.Type == JTokenType.Array)
            {
                // Handle arrays, converting objects inside the array
                JArray array = new JArray();
                foreach (var item in property.Value)
                {
                    if (item.Type == JTokenType.Object)
                    {
                        array.Add(ConvertKeysToSnakeCase((JObject)item));
                    }
                    else
                    {
                        array.Add(item);
                    }
                }
                newObj[snakeCaseKey] = array;
            }
            else
            {
                newObj[snakeCaseKey] = property.Value;
            }
        }

        return newObj;
    }

    static string CamelToSnake(string camelCase)
    {
        if (string.IsNullOrEmpty(camelCase))
            return camelCase;

        // Build snake_case from camelCase by inserting underscores before uppercase letters
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < camelCase.Length; i++)
        {
            char c = camelCase[i];
            if (char.IsUpper(c))
            {
                // For uppercase letters, add underscore if it's not the first character
                if (i > 0)
                    sb.Append('_');
                sb.Append(char.ToLower(c, CultureInfo.InvariantCulture));
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}