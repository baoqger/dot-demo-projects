using System;
using System.Collections.Generic;

namespace BinarySearchList
{
    public class CustomComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            // Custom comparison logic
            // For example, compare absolute values
            return Math.Abs(x).CompareTo(Math.Abs(y));
        }
    }

    class Program
    {
        static void Main()
        {
            IEnumerable<int> numbers2 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20, 30 };

            var target = numbers2.LastOrDefault((v) => v <= 0.1, 24);
            
            Console.WriteLine(target);

            IDictionary<string, KeyValuePair<DateTimeOffset, object>> a;


            Console.ReadLine();
            return;


            List<int> numbers = new List<int> { 1, 2, 4, 5, 6, 8, 9, 12, 30, 45, 50, 60, 70, 80, 99, 101, 1000, 2000, 3000 };

            // Sorting the list in ascending order
            numbers.Sort();

            // Using custom comparer for binary search
            CustomComparer comparer = new CustomComparer();

            // Performing binary search with custom comparer
            int index = numbers.BinarySearch(-55, comparer);

            if (index >= 0)
            {
                Console.WriteLine($"Element found at index: {index}");
            }
            else
            {
                Console.WriteLine("Element not found in the list.");
            }
        }
    }
}
