using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToleranceTunnelParser
{
    public class TorleranceTunnelDDP
    {
        [JsonProperty("schema_version")]
        public int SchemaVersion { get; set; }

        [JsonProperty("tolerance_tunnel")]
        public ToleranceTunnel ToleranceTunnel { get; set; }
    }

    public class ToleranceTunnel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("well_id")]
        public string WellId { get; set; }

        [JsonProperty("wellbore_id")]
        public string WellboreId { get; set; }

        [JsonProperty("trajectory_id")]
        public string TrajectoryId { get; set; }

        [JsonProperty("default_tunnel_radius")]
        public float DefaultTunnelRadius { get; set; }

        [JsonProperty("tolerance_lines_by_depth")]
        public DdpToleranceLineByDepth[] ToleranceLinesByDepth { get; set; }

        public long LastUpdateTicks { get; set; }
    }

    public class DdpToleranceLineByDepth
    {
        [JsonProperty("md_range")]
        public MdRange MdRange { get; set; }

        
        [JsonProperty("tolerance_lines")]
        public DdpToleranceLine[] ToleranceLines { get; set; }
    }

    public class MdRange
    {
        [JsonProperty("md_from")]
        public float MdFrom { get; set; }

        [JsonProperty("md_to")]
        public float MdTo { get; set; }
    }

    public class DdpToleranceLine
    {
        [JsonProperty("points")]
        public DdpToleranceLinePoint[] Points { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }

    public class DdpToleranceLinePoint
    {
        [JsonProperty("radius")]
        public float Radius { get; set; }
        [JsonProperty("azimuth")]
        public float Azimuth { get; set; }
        [JsonProperty("is_manual_input")]
        public bool IsManualInput { get; set; }
    }
}
