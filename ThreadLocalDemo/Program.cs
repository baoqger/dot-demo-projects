using System;
using System.Threading;


namespace ThreadLocalDemo
{

    public class Example
    {
        private static ThreadLocal<int> _threadLocalCounter
            = new ThreadLocal<int>(() => 0); // Initialize to 0 for each thread

        public static void IncrementCounter()
        {
            _threadLocalCounter.Value++; // Increment the counter for the current thread
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}, Counter: {_threadLocalCounter.Value}");
        }
    }


    public class Program
    {
        public static void Main()
        {
            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(Example.IncrementCounter);
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join(); // Wait for all threads to finish
            }
        }
    }

}
