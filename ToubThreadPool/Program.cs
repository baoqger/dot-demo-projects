// See https://aka.ms/new-console-template for more information

namespace ToubThreadingPool {
    public class Program
    {
        static void Main(string[] args)
        {
            ManagedThreadPool.QueueUserWorkItem(new WaitCallback(TestMethod));
            Console.Read();
        }

        static void TestMethod(object? o) { 
            Thread.Sleep(1000);
            Console.WriteLine("debug in thread...");
        }
    }

}
