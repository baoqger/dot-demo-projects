using System;

namespace environment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Get the value of the "MY_VARIABLE" environment variable
            string myVariableValue = Environment.GetEnvironmentVariable("MY_VARIABLE");

            // Check if the variable is null or empty
            if (string.IsNullOrEmpty(myVariableValue))
            {
                Console.WriteLine("The environment variable MY_VARIABLE is not set.");
            }
            else
            {
                Console.WriteLine("The value of MY_VARIABLE is: " + myVariableValue);
            }
        }
    }
}