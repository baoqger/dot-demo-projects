namespace DateTimeDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dic = new Dictionary<string, int>();
            if (dic.Count() == 0) {
                Console.WriteLine("empty dic");
            }

            Console.WriteLine("Hello, World!");
            DateTimeOffset? T1 = DateTimeOffset.UtcNow;
            DateTimeOffset? T2 = DateTimeOffset.UtcNow.AddMinutes(2);

            var a = T1.Value.UtcDateTime;


            TimeSpan? t = T1 - T2;

            if (t.Value.TotalMinutes >= 3)
            {
                Console.WriteLine("debug earlier than 3mins");
            }
            else {
                Console.WriteLine("debug no earlier than 3mins");
            }
        }
    }
}
