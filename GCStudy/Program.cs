using System;

namespace GCStudy
{
    class MyClass { }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating an object
            var obj = new MyClass();

            // Assigning 'obj' to a local variable
            var localVar = obj;

            // Checking if 'obj' and 'localVar' are rooted
            Console.WriteLine("Is 'obj' rooted? " + IsRooted(obj));
            Console.WriteLine("Is 'localVar' rooted? " + IsRooted(localVar));

            // Setting 'obj' to null
            obj = null;

            // Checking if 'obj' is still rooted (it's not since we set it to null)
            // Console.WriteLine("Is 'obj' rooted after setting it to null? " + IsRooted(obj));

            // Calling GC.Collect to force garbage collection
            GC.Collect();

            // Checking if 'localVar' is still rooted (it is since it holds a reference to the object)
            Console.WriteLine("Is 'localVar' rooted after garbage collection? " + IsRooted(localVar));
        }

        static bool IsRooted(object obj)
        {
            return GC.GetGeneration(obj) <= GC.GetGeneration(GC.MaxGeneration);
        }
    }
}
