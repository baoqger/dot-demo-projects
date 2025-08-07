using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareCementingPressureData
{
    public static class DataSetGenerator
    {
        public static Dictionary<string, Dictionary<string, List<CementingDepthBasedResult>>> GenerateDataset()
        {
            var annulusDepthBasedResults = new Dictionary<string, List<CementingDepthBasedResult>>();

            DateTimeOffset startTime = DateTimeOffset.Parse("2025-07-07T08:28:48.000Z");
            int totalTimePoints = 2000;

            Random rnd = new Random(42);

            for (int i = 0; i < totalTimePoints; i++)
            {
                DateTimeOffset currentTime = startTime.AddSeconds(i);
                string timeKey = currentTime.UtcDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");

                // Calculate length of pressure array for this time point
                // Linearly from 50 to 100 over 2000 increments
                int length = 50 + (int)((100 - 50) * (i / (double)(totalTimePoints - 1)));

                var entries = new List<CementingDepthBasedResult>(length);

                for (int mdIndex = 0; mdIndex < length; mdIndex++)
                {
                    double md = mdIndex * 10; // e.g. MD increments of 10 meters

                    // Generate pseudo-random but consistent values with some pattern
                    bool isInsidePipe = (mdIndex % 2 == 0);

                    double basePressure = 4300 + mdIndex * 10;
                    double pressure = basePressure + rnd.NextDouble() * 200; // 0-200 random noise

                    double baseTemp = 110 + mdIndex * 0.5;
                    double temperature = baseTemp + rnd.NextDouble() * 10;

                    double ecd = 1.0 + (mdIndex % 5) * 0.05;
                    double esd = 0.7 + (mdIndex % 3) * 0.05;

                    double porePressureEFD = 3400 + mdIndex * 5;
                    double fracturePressureEFD = 4900 + mdIndex * 5;

                    entries.Add(new CementingDepthBasedResult
                    {
                        MD = md,
                        IsInsidePipe = isInsidePipe,
                        Pressure = Math.Round(pressure, 2),
                        Temperature = Math.Round(temperature, 2),
                        ECD = Math.Round(ecd, 2),
                        ESD = Math.Round(esd, 2),
                        PorePressureEFD = Math.Round(porePressureEFD, 2),
                        FracturePressureEFD = Math.Round(fracturePressureEFD, 2),
                        DepthUnit = "meters",
                        PressureUnit = "psi",
                        TemperatureUnit = "Celsius",
                        DensityUnit = "ppg"
                    });
                }

                annulusDepthBasedResults[timeKey] = entries;
            }

            return new Dictionary<string, Dictionary<string, List<CementingDepthBasedResult>>>
            {
                { "AnnulusDepthBasedResults", annulusDepthBasedResults }
            };
        }
    }
}
