using System;
using System.Threading;

class Program
{
    private static int counter = 0;

    static void Main()
    {
        Thread[] threads = new Thread[10];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < 10000; j++)
                {
                    Interlocked.Increment(ref counter);
                    // counter++;
                }
            });
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("Final counter value: " + counter);
    }
}
