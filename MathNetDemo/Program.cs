using System.Numerics;
using Slb.Planck.Steering.Engine.Library.Modules;

namespace MathNetDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // double atf, sef;
            // ComputeATFAndSEF(out atf, out sef);
            double? dbl, tri;
            TestCallCompute(1, out dbl, out tri);
            Console.WriteLine("double: " + dbl + " tri: " + tri);
            Console.ReadLine();
        }

        static void ComputeATFAndSEF(out double atf, out double sef) {
            var toolfaces = new List<double> { 1, 2, 3, 4, 5 };
            var mds = new List<double> { 1, 2, 3, 4, 5 };
            var toolfaceComplex = toolfaces.Select(toolface => Complex.FromPolarCoordinates(1.0, toolface));
            // var a = Complex.FromPolarCoordinates(1.0, tool_face / 57.2958);
            Console.WriteLine("Hello, World!");
            sef = HighDefinitionDepthCalculator.CalculateHighDefinitionDepthBasedSef(toolfaceComplex, mds, out atf);
        }


        static void TestCallCompute(double x, out double? dbl, out double? tri) {
            double result;
            tri = TestCompute(x, out result);
            dbl = result;
        }

        static double TestCompute(double x, out double dbl) {
            dbl = x * 2;
            return x * 3;
        }
    }
}
