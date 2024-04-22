using System.Formats.Asn1;
using System.Runtime.InteropServices;

namespace DemoDump
{
    public class AllocateUnmanagedMemory
    {

        static IntPtr pointer;

        public void UnmanagedAllocation()
        {
            pointer = Marshal.AllocHGlobal(1024 * 1024 * 1024);
        }
    }

    public class AllocateManagedMemory {

        // Import the VirtualAlloc function from the Windows API
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        public void ManagedMemory(uint allocationSize, uint allocationType, uint protectFlags) {
            IntPtr allocatedMemory = VirtualAlloc(IntPtr.Zero, allocationSize, allocationType, protectFlags);
            if (allocatedMemory == IntPtr.Zero)
            {
                // VirtualAlloc failed, handle the error
                int errorCode = Marshal.GetLastWin32Error();
                Console.WriteLine($"VirtualAlloc failed with error code: {errorCode}");
            }
            else
            {
                // VirtualAlloc succeeded, use the allocated memory
                Console.WriteLine("Memory allocated successfully!");
                Console.WriteLine($"Allocated Memory Address: 0x{allocatedMemory.ToInt64():X}");
            }
        }
    }

    public class Program
    {
        public static int[] arr;
        public static AllocateUnmanagedMemory cls;
        public static AllocateManagedMemory clr;


        static void Main(string[] args)
        {
            const int GBSize = 1 * 1024 * 1024 * 1024 / sizeof(int);

            Console.WriteLine("Allocating");

            arr = new int[GBSize];

            // cls = new AllocateUnmanagedMemory();
            // cls.UnmanagedAllocation();

            const uint heapSize = 1 * 1024 * 1024 * 1024;
            clr = new AllocateManagedMemory();
            clr.ManagedMemory(heapSize, 0x1000, 0x04);

            Console.ReadLine();
        }
    }
}
