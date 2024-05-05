using System.Threading;
using System.IO;

namespace DelegateDemo
{
    internal class Program
    {
        public delegate int MathOperation(int a, int b);

        public delegate void ProgressReporter(int percentComplete);

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
            ProgressReporter p = WriteProgressToConsole;
            p += WriteProgressToFile;
            HardWork(p);

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
