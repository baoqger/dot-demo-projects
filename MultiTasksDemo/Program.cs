namespace MultiTasksDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Task<string> task1 = Task.FromResult("String result");
            Task<int> task2 = Task.FromResult(42);
            Task<List<string>> task3 = Task.FromResult(new List<string> { "item1", "item2" });

            var results = await Task.WhenAll(task1, task2, task3);
        }
    }
}
