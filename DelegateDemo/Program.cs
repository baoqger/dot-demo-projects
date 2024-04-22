namespace DelegateDemo
{
    internal class Program
    {
        delegate int MathOperation(int a, int b);

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
            // Create delegate instances and associate them with methods
            MathOperation add = Add;
            MathOperation subtract = Subtract;

            // Use the delegates to perform calculations
            int result1 = add(5, 3);        // Invokes Add method
            int result2 = subtract(8, 4);   // Invokes Subtract method

            // Display the results
            Console.WriteLine("Result of addition: " + result1);
            Console.WriteLine("Result of subtraction: " + result2);
        }
    }
}
