#nullable enable
using System;

namespace LibA
{
    public class TrajectoryDeviation
    {
        public double Left { get; set; }
        public double Right { get; set; }
        public double Above { get; set; }
        public double Below { get; set; }
        public string? Unit { get; set; }
    }
}
