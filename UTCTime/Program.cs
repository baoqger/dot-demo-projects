namespace UTCTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime targetTime = new DateTime(2023, 9, 20, 16, 48, 0);
            DateTime current =  DateTime.Now;

            TimeSpan timeSpan= targetTime - current;
            Console.WriteLine(timeSpan.TotalMinutes);
            Console.WriteLine(timeSpan.TotalSeconds);
        }
    }
}