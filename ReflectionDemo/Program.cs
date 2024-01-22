using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ReflectionDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            PropertyInfo[] properties = typeof(User).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine(property.Name);
            }
            foreach (var customAttribute in typeof(User).GetCustomAttributes()) {
                Console.WriteLine(customAttribute.ToString());
            }
        }
    }

    [ExcludeFromCodeCoverage]
    public class User { 
        public string Name { get; set; }
        public int Age { get; set; }
    }
}