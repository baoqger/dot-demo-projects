using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string inputFilePath = @"C:\\Users\\jbao6\\Desktop\\dev\\cementing\\file-replaytool-csv-job10\\output.csv";  // Change to your CSV file path

        try
        {
            string[] lines = File.ReadAllLines(inputFilePath);
            if (lines.Length == 0)
            {
                Console.WriteLine("Input file is empty.");
                return;
            }

            string headerLine = lines[0];
            string[] headers = headerLine.Split(',');

            int pumpStageTypeIndex = Array.IndexOf(headers, "PumpStageType");
            if (pumpStageTypeIndex == -1)
            {
                Console.WriteLine("Column 'PumpStageType' not found.");
                return;
            }

            // Initialize empty dictionary for counts
            var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Helper function to increment count for a given key
            void IncrementCount(string key)
            {
                if (counts.ContainsKey(key))
                {
                    counts[key]++;
                }
                else
                {
                    counts[key] = 1;
                }
            }

            // Process each data line
            for (int i = 2; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] columns = line.Split(',');

                if (columns.Length <= pumpStageTypeIndex)
                {
                    Console.WriteLine($"Skipping malformed line {i + 1}");
                    continue;
                }

                string category = columns[pumpStageTypeIndex];
                // Remove all whitespace in the string
                category = Regex.Replace(category, @"\s+", "");

                if (string.IsNullOrEmpty(category))
                {
                    IncrementCount("Empty");
                }
                else
                {
                    IncrementCount(category);
                }
            }

            // Print the results
            Console.WriteLine("Category counts in 'PumpStageType':");
            foreach (var kvp in counts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
