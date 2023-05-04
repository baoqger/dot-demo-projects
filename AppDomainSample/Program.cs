using System;
using System.Reflection;

namespace AppDomainSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the current application domain  
            AppDomain appDomain1 = AppDomain.CurrentDomain;
            Console.WriteLine("App Domain Name : '{0}'", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("ID of the Domain : '{0}'", AppDomain.CurrentDomain.Id);

            AppDomain newAppDomain = AppDomain.CreateDomain("NewAppDomain");
            newAppDomain.ExecuteAssembly(@"C:\develop\study\dotnet\DemoPro\ConsoleApp1\bin\Debug\ConsoleApp1.exe");
            // Use the path to load the assembly into the current application domain.  
            // Assembly a = Assembly.LoadFrom(@"C:\develop\study\dotnet\DemoPro\ConsoleApp1\bin\Debug\ConsoleApp1.exe");
            // Get the Program class of the ConsoleApp1 to use here.  
            // Type programClass = a.GetType("ConsoleApp1.Program");
            // Get the method GetAssemblyName method to make a call.  
            // MethodInfo getAssemblyName = programClass.GetMethod("GetAssemblyName");
            // Create an instance o.  
            // object programObject = Activator.CreateInstance(programClass);
            // Execute the GetAssemblyName method.  
            // getAssemblyName.Invoke(programObject, null);

            Console.ReadKey();
        }
    }
}