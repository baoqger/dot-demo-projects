namespace MemBar
{
    internal class Program
    {
        volatile static bool complete = false;
        static void Main(string[] args)
        {
            
            var t = new Thread(() => { 
                bool toggle = false;

                while (!complete)
                {
                    // Thread.MemoryBarrier();
                    toggle = !toggle;
                }
            });
            t.Start();
            Thread.Sleep(1000);

            complete = true;
            t.Join();
            Console.WriteLine("complete");
        }
    }
}
