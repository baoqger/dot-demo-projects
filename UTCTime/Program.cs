using Slb.Prism.Shared.Contract.ComputationEngine.DataModel.ContextModel;
using Slb.Prism.Shared.Library.ComputationEngine.DataModel;
namespace UTCTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(2023, 10, 20, 12, 0, 0, TimeSpan.FromHours(0));
            var a = dateTimeOffset.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
            Console.WriteLine(a);

            var current = DateTimeOffset.UtcNow;
            var before = current.Subtract(new TimeSpan(1, 0, 0));
            var after = current.Add(new TimeSpan(1, 0, 0));

            Console.WriteLine("current" + current.ToString());
            Console.WriteLine("before" + before.ToString());
            Console.WriteLine("after" + after.ToString());

            var s = new SlideSheet();
        }
    }
}