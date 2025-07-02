using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program
{


    static void Main(string[] args)
    {
        // Input and output file paths
        string inputFilePath = @"C:\\Users\\jbao6\\Desktop\\dev\\cementing\\file-replaytool-csv-job10\\cementing_hydraulics_inputchannels.csv";   // Change this to your actual CSV path
        string outputFilePath = @"C:\\Users\\jbao6\\Desktop\\dev\\cementing\\file-replaytool-csv-job10\\output.csv";

        try
        {
            // Read all lines from input CSV
            string[] lines = File.ReadAllLines(inputFilePath);

            if (lines.Length == 0)
            {
                Console.WriteLine("Input file is empty.");
                return;
            }

            // Find index of PumpStageType column
            string headerLine = lines[0];
            string unit = lines[1];
            string[] headers = headerLine.Split(',');

            int pumpStageTypeIndex = Array.IndexOf(headers, "PumpStageType");
            if (pumpStageTypeIndex == -1)
            {
                Console.WriteLine("Column 'PumpStageType' not found.");
                return;
            }

            // Prepare output lines (start with header)
            List<string> outputLines = new List<string> { headerLine, unit };

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
                category = Regex.Replace(category, @"\s+", "");

                if (string.IsNullOrEmpty(category))
                {
                    // Keep empty as is
                    columns[pumpStageTypeIndex] = "";
                }
                else
                {
                    // Convert category to number based on enum
                    int numericValue = CategoryToNumber(category);
                    columns[pumpStageTypeIndex] = numericValue.ToString();
                    // If unknown category (0), keep original string
                }

                // Join columns back to CSV line
                string newLine = string.Join(",", columns);
                outputLines.Add(newLine);
            }

            // Write to output file
            File.WriteAllLines(outputFilePath, outputLines);

            Console.WriteLine($"Conversion complete. Output saved to: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static int CategoryToNumber(string category)
    {
        return category switch
        {
            "Fluid" => (int)PumpStageType.Fluid,
            "Pause" => (int)PumpStageType.Pause,
            "Plug" => (int)PumpStageType.Plug,
            "TopPlug" => (int)PumpStageType.TopPlug,
            "BottomPlug" => (int)PumpStageType.BottomPlug,
            "Dart" => (int)PumpStageType.Dart,
            "ShutIn" => (int)PumpStageType.ShutIn,
            "TopPlugBump" => (int)PumpStageType.TopPlugBump,
            _ => -1 // Default or unknown category
        };
    }

    // Enum definition for PumpStageType
    public enum PumpStageType
    {
        Fluid,
        Pause,
        Plug,
        TopPlug,
        BottomPlug,
        Dart,
        ShutIn,
        TopPlugBump
    }
}