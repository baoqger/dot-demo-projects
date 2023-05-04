using System;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Assembly Loading Demo.");
            Console.WriteLine("App Domain Name : '{0}'", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("ID of the Domain : '{0}'", AppDomain.CurrentDomain.Id);
        }

        public string GetAssemblyName()
        {
            Console.WriteLine("This is ConsoleApp1!");
            Console.WriteLine("App Domain Name : '{0}'", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("ID of the Domain : '{0}'", AppDomain.CurrentDomain.Id);
            return AppDomain.CurrentDomain.FriendlyName;
        }
    }
}
