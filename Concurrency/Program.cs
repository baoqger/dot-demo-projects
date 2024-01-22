
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Concurrency
{
    public class Program
    {
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> locks = new ConcurrentDictionary<string, SemaphoreSlim>();

        static void Main()
        {

            var ids = new[] { "id1", "id2", "id3", "id1", "id2" };

            var tasks = new List<Task>();
            foreach (var id in ids)
            {
                tasks.Add(Task.Run(async () => await UpdateObjectAsync(id)));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("dictionary size: " + locks.Count);
        }

        static async Task UpdateObjectAsync(string id)
        {
            // Acquire the semaphore associated with the ID
            var semaphore = locks.GetOrAdd(id, _ => new SemaphoreSlim(1, 1));
            await semaphore.WaitAsync(); // Asynchronously wait for the semaphore
            try
            {
                // Perform the update operation
                // ...
                // Simulate some work
                await Task.Delay(2000);
                Console.WriteLine($"Updated object with ID: {id}");
            }
            finally
            {
                locks.TryRemove(id, out _);
                semaphore.Release(); // Release the semaphore when the update is complete
            }
        }
    }

}


