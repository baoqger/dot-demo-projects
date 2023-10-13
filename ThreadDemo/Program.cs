
using System;
using System.Threading;

namespace ThreadDemo
{
    public class Program
    {
        static List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

        static void Main()
        {
            // Create two threads that read and print the numbers list
            Thread thread1 = new Thread(ReadNumbers);
            Thread thread2 = new Thread(ReadNumbers);

            // Start the threads
            thread1.Start();
            thread2.Start();

            // Wait for both threads to complete
            thread1.Join();
            thread2.Join();

            Console.WriteLine("Read operations complete.");
        }

        static void ReadNumbers()
        {
            foreach (int number in numbers)
            {
                // Simulate some processing on the current element
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Processing number {number}");

                // Simulate some delay to make the processing more evident
                Thread.Sleep(100);
            }
        }

        static void EnumerateNumbers(List<int> numbers)
        {
            // Obtain an enumerator from the numbers list
            IEnumerator<int> enumerator = numbers.GetEnumerator();

            while (enumerator.MoveNext())
            {
                int number = enumerator.Current;

                // Simulate some processing on the current element
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Processing number {number}");

                // Simulate some delay to make the processing more evident
                Thread.Sleep(100);
            }
        }

        static void EnumerateNumbers(HashSet<int> numbers)
        {
                foreach (int number in numbers)
                {
                    // Simulate some processing on the current element
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Processing number {number}");

                    // Simulate some delay to make the processing more evident
                    Thread.Sleep(100);
                }
        }

        static void IncrementCounter(ref int counter)
        {
            for (int i = 0; i < 100000; i++)
            {
                counter++;
            }
        }
    }


}