using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Runtime.InteropServices.ObjectiveC;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PrepareCementingPressureData
{
    public static class DepthSeriesConverter
    {
        /// <summary>
        /// Convert time series dictionary to depth series dictionary with arrays of entries per depth.
        /// </summary>
        /// <param name="timeSeriesData">Dictionary keyed by string and DateTimeOffset with object array values</param>
        /// <returns>Dictionary keyed by string and double (MD) with list of values</returns>
        public static ConcurrentDictionary<string, Dictionary<string, CementingTimeBasedProfile>>
            ConvertTimeSeriesToDepthSeriesStronglyTyped(
                Dictionary<string, Dictionary<DateTimeOffset, object>> timeSeriesData)
        {
            var depthSeriesData = new ConcurrentDictionary<string, Dictionary<string, CementingTimeBasedProfile>>();

            foreach (var outerKvp in timeSeriesData)
            {
                string outerKey = outerKvp.Key;
                var timeDict = outerKvp.Value;

                var depthDict = new Dictionary<string, CementingTimeBasedProfile>();

                // Lock object to protect depthDict updates
                var depthDictLock = new object();

                Parallel.ForEach(timeDict, timeKvp =>
                {
                    DateTimeOffset timeIndex = timeKvp.Key;
                    var results = (CementingPressureProfile)timeKvp.Value;

                    foreach (var result in results)
                    {
                        var depthKey = result.MD.ToString();

                        // Lock to ensure thread-safe access to depthDict and its profiles
                        lock (depthDictLock)
                        {
                            if (!depthDict.ContainsKey(depthKey))
                            {
                                depthDict[depthKey] = new CementingTimeBasedProfile();
                            }
                        }

                        // Add result outside lock for performance
                        // **But CementingTimeBasedProfile.Add must be thread-safe or locked here**

                        // To be safe, lock again for Add (assuming CementingTimeBasedProfile.Add is NOT thread-safe)
                        lock (depthDictLock)
                        {
                            depthDict[depthKey].Add(new CementingTimeBasedResult
                            {
                                TimeIndex = timeIndex,
                                IsInsidePipe = result.IsInsidePipe,
                                Pressure = result.Pressure,
                                Temperature = result.Temperature,
                                ECD = result.ECD,
                                ESD = result.ESD,
                                PorePressureEFD = result.PorePressureEFD,
                                FracturePressureEFD = result.FracturePressureEFD,
                                DepthUnit = result.DepthUnit,
                                PressureUnit = result.PressureUnit,
                                TemperatureUnit = result.TemperatureUnit,
                                DensityUnit = result.DensityUnit
                            });
                        }
                    }
                });

                // Sort each CementingTimeBasedProfile's results by TimeIndex ascending
                foreach (var kvp in depthDict)
                {
                    kvp.Value.Sort((a, b) => a.TimeIndex.CompareTo(b.TimeIndex));
                }

                depthSeriesData[outerKey] = depthDict;
            }
            return depthSeriesData;
        }
    }
}
