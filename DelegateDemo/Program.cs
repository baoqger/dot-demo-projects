using System.Threading;
using System.IO;
using System.Net.Http.Headers;
using System.Collections.Concurrent;

namespace DelegateDemo
{
    internal class Program
    {
        public delegate int MathOperation(int a, int b);

        public delegate void ProgressReporter(int percentComplete);

        public static ConcurrentDictionary<int, string> stringMap = new ConcurrentDictionary<int, string>();

        // Method to add two numbers
        static int Add(int a, int b)
        {
            return a + b;
        }

        // Method to subtract two numbers
        static int Subtract(int a, int b)
        {
            return a - b;
        }

        static void Main(string[] args)
        {
            // multicast delegate
            // ProgressReporter p = WriteProgressToConsole;
            // p += WriteProgressToFile;
            // HardWork(p);

            var a = GetString(1);
            var b = GetString(2);
            var c = GetString(3);
            Console.WriteLine(a + b + c);
        }

        public static string GetString(int key) {
            return stringMap.GetOrAdd(key, delegate
            {
                if (key % 2 == 0)
                {
                    return "even";
                }
                else {
                    return "odd";
                }
            });
        }

        public static void HardWork(ProgressReporter p) {
            for (int i = 0; i < 10; i++) {
                p(i * 10);
                Thread.Sleep(100);
            }
        }

        static void WriteProgressToConsole(int percentComplete) => Console.WriteLine(percentComplete);
        static void WriteProgressToFile(int percentComplete) => File.WriteAllText(@"C:\Users\jbao6\Desktop\progress.txt", percentComplete.ToString());
    }
}
