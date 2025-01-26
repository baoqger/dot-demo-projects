namespace MultiTaskDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Task<string> task1 = Task.FromResult("String result");
            Task<int> task2 = Task.FromResult(42);
            Task<List<string>> task3 = Task.FromResult(new List<string> { "item1", "item2" });

            var results = await WhenAllCompleted(task1, task2);

            Console.WriteLine("debug xxxx" + results);
        }

        static async Task<(T1, T2)> WhenAllCompleted<T1, T2>(Task<T1> t1, Task<T2> t2)
        {
            await Task.WhenAll(t1, t2);
            return (await t1, await t2);
        }
    }
}
