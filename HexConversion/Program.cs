namespace HexConversion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string prefixedHex = "0x111";
            // this works, and returns 1322173
            int intValue = Convert.ToInt32(prefixedHex, 16);
            Console.WriteLine("Hello, World!" + intValue);
        }
    }
}
