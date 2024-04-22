using System.Runtime;
using System.Text.RegularExpressions;

namespace ProcessDumpFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // managedHeap();
            commitedMemoryByType();
            // getCLRHeap();
            bool isServerGC = GCSettings.IsServerGC;
            Console.WriteLine("IsServerGC: " + isServerGC);
        }

        static void managedHeap() {
            string filePath = "C:\\Users\\jbao6\\Desktop\\heapusage.txt";
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                int counter = 0;
                int total = 0;

                // Read and process each line until the end of the file
                while ((line = reader.ReadLine()) != null)
                {
                    // Process the line (replace this with your own logic)
                    string[] strings = Regex.Split(line, @"\s+");
                    Console.WriteLine(strings[2]);
                    total += int.Parse(strings[2]);
                    counter++;
                }
                Console.WriteLine("line number: " + counter);
                Console.WriteLine("total size: " + total + "bytes");
                Console.WriteLine("total size: " + total / (1024 * 1024) + "mb");

                var entire = getNativeHeapMemory();
                Console.WriteLine("entire heap: " + entire + "kb");
                Console.WriteLine("entire heap: " + entire / 1024 + "mb");


            }
        }

        static int  getNativeHeapMemory() {
            return 65128 + 64 + 1280 + 1280 + 60 + 60 + 260 + 15604 + 64168 + 1280 + 15604 + 3324 + 1280 + 260 + 7416;
        }

        static void getCLRHeap() {
            var sizeInBytes = 495616 + 495616 + 21368832 + 1646592 + 24006656;
            var sizeInMB = sizeInBytes / (1024 * 1024);
            Console.WriteLine("CLR heap size: " + sizeInMB + "MB");
        }
        static void commitedMemoryByType() {

            string filePath = "C:\\Users\\jbao6\\Desktop\\commitmemorybytype.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                int counter = 0;
                uint total = 0;
                var memoryByType = new Dictionary<string, uint>();

                // Read and process each line until the end of the file
                while ((line = reader.ReadLine()) != null)
                {
                    // Process the line (replace this with your own logic)
                    string[] strings = Regex.Split(line, @"\s+");
                    // Console.WriteLine(strings[1]);
                    uint intValue = Convert.ToUInt32(strings[1], 16);
                    Console.WriteLine(strings[1] + " " + intValue);
                    string type = strings[3];
                    if (!memoryByType.ContainsKey(type))
                    {
                        memoryByType.Add(type, intValue);
                    }
                    else { 
                        var cur = memoryByType[type];
                        memoryByType[type] = cur + intValue;
                    }
                    total += intValue;
                    counter++;
                    // Console.WriteLine("line number: " + counter);
                    // Console.WriteLine("total size: " + total + "bytes");
                }

                Console.WriteLine("line number: " + counter);
                Console.WriteLine("total size: " + total + "bytes");
                Console.WriteLine("total size: " + (float)total / (1024 * 1024) + "mb");
                Console.WriteLine("total size: " + (float)total / (1024 * 1024 * 1024) + "GB");

                Console.WriteLine("Memory by Type: ");
                float newTotal = 0;
                foreach (var pair in memoryByType) {
                    // Console.WriteLine("type: " + pair.Key + " " + pair.Value + "bytes");
                    Console.WriteLine("type: " + pair.Key + " " + (float)pair.Value / (1024 * 1024) + "MB");
                    // Console.WriteLine("type: " + pair.Key + " " + (float)pair.Value / (1024 * 1024 * 1024) + "GB");
                }
            }

        }
    }
}
