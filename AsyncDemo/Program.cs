namespace AsyncDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Task.WhenAll(DoSomethingAsync(1), DoSomethingAsync2(1));
            await Task.WhenAll(
                Task.Run(() => DoSomethingAsync(1)),
                Task.Run(() => DoSomethingAsync2(1))
            );

            
        }

        static public Task DoSomethingAsync(int n) {
            Console.WriteLine("Start in method 1");

            Thread.Sleep(1000 * n);

            Console.WriteLine("End in method 1");

            return Task.FromResult(true);
        }

        static public Task DoSomethingAsync2(int n)
        {
            Console.WriteLine("Start in method 2");

            Thread.Sleep(2000 * n);

            Console.WriteLine("End in method 2");

            return Task.FromResult(true);
        }

        static public async Task<string> GetStringAsync()
        {
            await Task.Delay(1000);
            return "Hello";
        }

        static public async Task<int> GetIntAsync()
        {
            await Task.Delay(2000);
            return 42;
        }
    }


}