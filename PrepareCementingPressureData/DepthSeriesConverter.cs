using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PrepareCementingPressureData
{
    public static class DepthSeriesConverter
    {
        /// <summary>
        /// Convert time series dictionary to depth series dictionary with arrays of entries per depth.
        /// </summary>
        /// <param name="timeSeriesData">Dictionary keyed by string and DateTimeOffset with dynamic array values</param>
        /// <returns>Dictionary keyed by string and double (MD) with list of dynamic values</returns>
        public static Dictionary<string, Dictionary<double, CementingTimeBasedProfile>>
            ConvertTimeSeriesToDepthSeriesStronglyTyped(
                Dictionary<string, Dictionary<DateTimeOffset, dynamic>> timeSeriesData)
        {
            var depthSeriesData = new Dictionary<string, Dictionary<double, CementingTimeBasedProfile>>();

            foreach (var outerKvp in timeSeriesData)
            {
                string outerKey = outerKvp.Key;
                var timeDict = outerKvp.Value;

                var depthDict = new Dictionary<double, CementingTimeBasedProfile>();

                foreach (var timeKvp in timeDict)
                {
                    DateTimeOffset timeIndex = timeKvp.Key;
                    var results = (CementingPressureProfile)timeKvp.Value;

                    foreach (var result in results)
                    {
                        if (!depthDict.ContainsKey(result.MD))
                        {
                            depthDict[result.MD] = new CementingTimeBasedProfile();
                        }

                        // Create a new object with TimeIndex property
                        var resultWithTime = new CementingTimeBasedResult
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
                            PressureUnit  = result.PressureUnit,
                            TemperatureUnit = result.TemperatureUnit,
                            DensityUnit = result.DensityUnit
                        };

                        depthDict[result.MD].Add(resultWithTime);
                    }
                }

                depthSeriesData[outerKey] = depthDict;
            }

            return depthSeriesData;
        }
    }
}
