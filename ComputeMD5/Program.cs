using System;
using System.Text;
using System.Diagnostics; // Include this namespace for Stopwatch
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace MD5HashExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load the JSON file
            string jsonFilePath = "C:\\develop\\study\\test\\nam-well1.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            // Parse the JSON data to JToken
            JToken jsonToken = JToken.Parse(jsonString);
            var count = jsonToken.Count();
            Console.WriteLine($"the number of object is : {count}");

            // Start the stopwatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Compute the MD5 hash
            string md5Hash = ComputeMD5Hash(jsonString);

            // Stop the stopwatch
            stopwatch.Stop();

            // Output the hash
            Console.WriteLine($"MD5 Hash: {md5Hash}");

            Console.WriteLine($"Time taken to compute MD5 Hash: {stopwatch.ElapsedMilliseconds} ms");
        }

        static string ComputeMD5Hash(string input)
        {
            // Create an MD5 hash object
            using (MD5 md5 = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                string md5String = Convert.ToBase64String(hashBytes);
                return md5String;
            }
        }
    }
}
