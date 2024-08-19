namespace DateTimeDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            DateTimeOffset T1 = DateTimeOffset.Now;
            DateTimeOffset T2 = DateTimeOffset.Now.AddMinutes(2);

            TimeSpan t = T1 - T2;

            if (t.TotalMinutes >= 3)
            {
                Console.WriteLine("debug earlier than 3mins");
            }
            else {
                Console.WriteLine("debug no earlier than 3mins");
            }
        }
    }
}
