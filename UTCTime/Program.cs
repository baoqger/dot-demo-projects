namespace UTCTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(2023, 10, 20, 12, 0, 0, TimeSpan.FromHours(0));
            var a = dateTimeOffset.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
            Console.WriteLine(a);
        }
    }
}