using System.Diagnostics;
using System.IO;

namespace ProcessNewWindow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var binFolder = @"C:\Users\jbao6\Desktop\powershell";
            var arguments = Path.Combine(binFolder, "testLivingdoc.ps1");
            arguments = "-command " + arguments;
            Console.WriteLine($"E2E start with argument: {arguments}");

            var startInfo = new ProcessStartInfo
            {
                FileName = @"powershell",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = false,
                UseShellExecute = false
            };

            using var p = Process.Start(startInfo);
            var stdError = p.StandardError.ReadToEnd();
            var stdOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            if (!string.IsNullOrEmpty(stdError))
                Console.WriteLine($"powershell running error: {stdError}");
            if (!string.IsNullOrEmpty(stdOutput))
                Console.WriteLine($"powershell running result: {stdOutput}");
        }
    }
}
