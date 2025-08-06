using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareCementingPressureData
{
    public class CementingDepthBasedResult
    {
        public double MD { get; set; }
        public bool IsInsidePipe { get; set; }
        public double Pressure { get; set; }
        public double Temperature { get; set; }
        public double ECD { get; set; }
        public double ESD { get; set; }
        public double PorePressureEFD { get; set; }
        public double FracturePressureEFD { get; set; }
        public string DepthUnit { get; set; }
        public string PressureUnit { get; set; }
        public string TemperatureUnit { get; set; }
        public string DensityUnit { get; set; }
    }

    public class CementingPressureProfile : List<CementingDepthBasedResult>
    {
    }

    public class CementingTimeBasedProfile : List<CementingTimeBasedResult>
    {
    }

    public class CementingTimeBasedResult
    {
        public DateTimeOffset TimeIndex { get; set; }
        public bool IsInsidePipe { get; set; }
        public double Pressure { get; set; }
        public double Temperature { get; set; }
        public double ECD { get; set; }
        public double ESD { get; set; }
        public double PorePressureEFD { get; set; }
        public double FracturePressureEFD { get; set; }
        public string DepthUnit { get; set; }
        public string PressureUnit { get; set; }
        public string TemperatureUnit { get; set; }
        public string DensityUnit { get; set; }
    }
}
