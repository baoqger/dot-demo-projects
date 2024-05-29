using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCLR4
{
    public class Program
    {
        static Stopwatch watch = new Stopwatch();
        static int pendingTask;

        static void Main(string[] args)
        {
            const int MaxValue = 1000000000;

            watch.Restart();
            int numTasks = Environment.ProcessorCount;
            pendingTask = numTasks;
            int perThreadCount = MaxValue / numTasks;
            int perThreadLeftover = MaxValue % numTasks;

            var tasks = new List<Task<long>>(new Task<long>[numTasks]);
            // var tasks = new Task<long>[numTasks];

            for (int i = 0; i < numTasks; i++)
            {
                int start = i * perThreadCount;
                int end = (i + 1) * perThreadCount;
                if (i == numTasks - 1)
                {
                    end += perThreadLeftover;
                }
                tasks[i] = Task.Run(() => {
                    long threadSum = 0;
                    for (int j = start; j <= end; j++)
                    {
                        threadSum += (long)Math.Sqrt(j);
                    }
                    return threadSum;
                });
                tasks[i].ContinueWith(OnTaskEnd);
            }
            Task.WaitAll(tasks.ToArray());
        }

        private static void OnTaskEnd(Task<long> task)
        {
            Console.WriteLine("Thread sum: {0} ", task.Result);
            if (Interlocked.Decrement(ref pendingTask) == 0)
            {
                watch.Stop();
                Console.WriteLine("Task: {0} ", watch.Elapsed);
            }
        }
    }
}
