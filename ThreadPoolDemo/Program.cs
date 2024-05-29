using System;
using System.Threading;

namespace ThreadPoolApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            int workerThreads;
            int portThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out portThreads);
            Console.WriteLine("\nMaximum worker threads: \t{0}" + "\nMaximum completion port threads: {1}", workerThreads, portThreads);

            int minWorker, minIOC;
            ThreadPool.GetMinThreads(out minWorker, out minIOC);
            Console.WriteLine("\nMinimum worker threads: \t{0}" + "\nMaximum completion port threads: {1}", minWorker, minIOC);

            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(MyMethod!));
            }
            Console.Read();
        }

        public static void MyMethod(object obj)
        {
            Thread thread = Thread.CurrentThread;
            string message = $"Background: {thread.IsBackground}, Thread Pool: {thread.IsThreadPoolThread}, Thread ID: {thread.ManagedThreadId}";
            Thread.Sleep(5000);
            Console.WriteLine(message);
        }
    }
}