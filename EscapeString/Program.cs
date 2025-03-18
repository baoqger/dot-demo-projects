using System;
using Newtonsoft.Json;
namespace EscapeString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var subTypes = new string[] { "Computation.BitProjection" };
            var converted = subTypes?.Select(t => System.Security.SecurityElement.Escape(t.Capitalize())).ToArray();

            var str = "{\"Station\":{\"Above\":-18.8941,\"Left\":-0.6954,\"Forward\":0.0,\"OutOfRange\":false,\"CtCt\":18.9069,\"RefPlanStation\":{\"MD\":497.4469400884152,\"TVD\":496.0155,\"Incl\":0.21247876346845573,\"Azi\":5.9388441356577761},\"TCProjection\":null,\"Tortuosity\":0.0,\"NS\":0.0,\"EW\":0.0,\"DLS\":0.0,\"BR\":0.0,\"TR\":0.0,\"MD\":500.0,\"TVD\":500.0,\"Incl\":0.0,\"Azi\":0.0},\"Message\":\"\",\"ProjectionMethod\":\"straight\"}";

            var data = JsonConvert.DeserializeObject<BitProjection>(str);

            Console.WriteLine(converted);
        }
    }

    public class BitProjection
    {
        public ActualStation Station { get; set; }
        public string Message { get; set; }            // reason for not computing the bit projection
        public string ProjectionMethod { get; set; }	// either "simulation" or "straight"
    }

    public static class StringExtensions
    {
        public static string Capitalize(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }

    public class StationBase
    {
        /// <summary>
        /// MD
        /// </summary>
        public double MD { get; set; }
        /// <summary>
        /// inclination
        /// </summary>
        public double Incl { get; set; }
        /// <summary>
        /// azimuth
        /// </summary>
        public double Azi { get; set; }
        /// <summary>
        /// TVD
        /// </summary>
        public double TVD { get; set; }
        /// <summary>
        /// NS
        /// </summary>
        public double NS { get; set; }
        /// <summary>
        /// EW
        /// </summary>
        public double EW { get; set; }
        /// <summary>
        /// Dogleg Severity
        /// </summary>
        public double DLS { get; set; }
        /// <summary>
        /// Build rate
        /// </summary>
        public double? BR { get; set; }
        /// <summary>
        /// Turn rate
        /// </summary>
        public double? TR { get; set; }
    }

    public class ActualStation : StationBase
    {
        public double Above { get; set; }
        public double Left { get; set; }
        public double Forward { get; set; }
        public bool OutOfRange { get; set; }
        public double CtCt { get; set; }
        public double? Tortuosity { get; set; } // Dog Leg cumulation 
        public PlannedStation RefPlanStation { get; set; }
        public StationProjectionTC TCProjection { get; set; }

    }

    public class PlannedStation : StationBase
    {

    }

    public sealed class StationProjectionTC // Projection in the traveling cylinder
    {
        public double Radius { get; set; }
        public double Azimuth { get; set; }
        public bool OutOfTunnel { get; set; }
    }
}
