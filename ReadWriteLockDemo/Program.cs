using System;
using System.Collections.Generic;
using System.Threading;

class Cache<TKey, TValue>
{
    private readonly Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();
    private readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();

    public TValue GetOrCompute(TKey key, Func<TValue> computeValue)
    {
        lockSlim.EnterReadLock();
        try
        {
            if (cache.ContainsKey(key))
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - Cache hit for key: {key}");
                return cache[key];
            }
            else {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - Cache miss for key: {key}");
                
            }
        }
        finally
        {
            lockSlim.ExitReadLock();
        }

        // If the value is not in the cache, compute it
        TValue value = computeValue();

        lockSlim.EnterWriteLock();
        try
        {
            // Check again if another thread already added the value while waiting for the write lock
            if (!cache.ContainsKey(key))
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - Adding value to cache for key: {key}");
                cache[key] = value;
            }
        }
        finally
        {
            lockSlim.ExitWriteLock();
        }

        return value;
    }
}

class Program
{
    static void Main()
    {
        Cache<int, string> cache = new Cache<int, string>();

        // Run multiple threads to access the cache concurrently
        for (int i = 0; i < 5; i++)
        {
            int key = 42; // Same key for all threads
            ThreadPool.QueueUserWorkItem(_ =>
            {
                string value = cache.GetOrCompute(key, () =>
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - Computing value for key: {key}");
                    Thread.Sleep(1000); // Simulate expensive computation
                    return $"Value for key {key}";
                });

                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - Retrieved value: {value}");
            });
        }

        // Wait for all threads to complete
        Thread.Sleep(5000);
    }
}