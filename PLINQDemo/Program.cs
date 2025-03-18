
using System.Diagnostics;

namespace PLINQDemo
{
    public class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = Enumerable.Range(0, 100)
                .AsParallel()
                .Select(Compute)
                .Sum();
            Console.WriteLine(result);

            Console.WriteLine($"It took: {stopwatch.ElapsedMilliseconds} ms to run");
        }

        static decimal Compute(int value)
        {
            var randomMilliseconds = random.Next(10, 50);
            var end = DateTime.Now + TimeSpan.FromMilliseconds(randomMilliseconds);

            while (DateTime.Now < end) { }

            return value + 0.5m;
        }
    }
}
