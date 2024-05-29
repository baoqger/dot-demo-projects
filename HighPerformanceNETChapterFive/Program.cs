using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace HighPerformanceNETChapterFive
{
    public class Program
    {
        static Stopwatch watch = new Stopwatch();
        static int pendingTask;

        static void Main(string[] args)
        {
            // TestContinue();
            // TestCancellation();
            // TestExceptionHandling();
            TestTPLDataFlow(@"C:\Users\jbao6\Downloads\txt");
        }

        private static void TestTPLDataFlow(string inputPath) { 
            HashSet<string> IgnoreWords = new HashSet<string>() { "a", "an", "the", "and", "of", "to"};
            Regex WordRegex = new Regex("[a-zA-Z]+", RegexOptions.Compiled);

            int fileCount = Directory.GetFiles(inputPath, "*.txt").Length;

            var getFileNames = new TransformManyBlock<string, string>(
                path => {
                    return Directory.GetFiles(path, "*.txt");
                });

            var getFileContents = new TransformBlock<string, string>(
                async (filename) => {
                    Console.WriteLine("Begin: getFileContents of file: " + filename);
                    using (var streamReader = new StreamReader(filename))
                    { 
                        var result = await streamReader.ReadToEndAsync();
                        Console.WriteLine("End: getFileContents");
                        return result;
                    }
                }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = Environment.ProcessorCount });

            var analyzeContents = new TransformBlock<string, Dictionary<string, ulong>>(
                    contents => {
                        Console.WriteLine("Begin: analyzeContents");
                        var frequencies = new Dictionary<string, ulong>(10000, StringComparer.OrdinalIgnoreCase);

                        var matches = WordRegex.Matches(contents);
                        foreach (var match in matches)
                        {
                            ulong currentValue;
                            if (!frequencies.TryGetValue(match.ToString(), out currentValue)) 
                            {
                                currentValue = 0;    
                            }
                            frequencies[match.ToString()] = currentValue + 1;
                        }
                        Console.WriteLine("End: analyzeContents");
                        return frequencies; 
                    }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = Environment.ProcessorCount });

            var eliminateIgnoredWords = new TransformBlock<Dictionary<string, ulong>, Dictionary<string, ulong>>(
                    input => {
                        foreach (var word in IgnoreWords)
                        { 
                            input.Remove(word);
                        }
                        return input;
                    }
                );

            var batch = new BatchBlock<Dictionary<string, ulong>>(fileCount);

            // This is one point of synchronization
            // all processing converges on this 
            var combineFrequencies = new TransformBlock<Dictionary<string, ulong>[], List<KeyValuePair<string, ulong>>>(
                    inputs => {
                        Console.WriteLine("Begin: combiningFrequencies.");
                        var sortedList = new List<KeyValuePair<string, ulong>>();
                        var combinedFrequencies = new Dictionary<string, ulong>(10000, StringComparer.OrdinalIgnoreCase);

                        foreach (var input in inputs)
                        {
                            foreach (var kvp in input) 
                            {
                                ulong currentFrequency;
                                if (!combinedFrequencies.TryGetValue(kvp.Key, out currentFrequency))
                                { 
                                    currentFrequency = 0;
                                }
                                combinedFrequencies[kvp.Key] = currentFrequency + kvp.Value;
                            }
                        }

                        foreach (var kvp in combinedFrequencies)
                        {
                            sortedList.Add(kvp);
                        }
                        sortedList.Sort((a, b) => { 
                            return -a.Value.CompareTo(b.Value);
                        });
                        Console.WriteLine("End: combineFrequencies.");

                        return sortedList;
                    }, new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 1});

            var printTopTen = new ActionBlock<List<KeyValuePair<string, ulong>>>(
                    input => {
                        for (int i = 0; i < 10; i++)
                        {
                            Console.WriteLine($"{input[i].Key} - {input[i].Value}");
                        }
                    }
                );

            // hook up blocks
            getFileNames.LinkTo(getFileContents);
            getFileContents.LinkTo(analyzeContents);
            analyzeContents.LinkTo(eliminateIgnoredWords);
            eliminateIgnoredWords.LinkTo(batch);
            batch.LinkTo(combineFrequencies);
            combineFrequencies.LinkTo(printTopTen);

            // start the process 
            getFileNames.Post(inputPath);

            // mark complete
            printTopTen.Completion.Wait();
        }

        private static void TestExceptionHandling() {
            Task<int>.Factory.StartNew(() =>
            {
                int x = 42;
                int y = 0;
                return x / y;
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    ExceptionUtils.LogException(t.Exception);
                }
                else {
                    int val = t.Result;
                    Console.WriteLine("the result is: " +  val);
                }

            }).Wait();
        }

        private static void TestCancellation()
        {
            var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Task task = Task.Run(() => 
            {
                while (true) {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation requested");
                        return;
                    }
                    Thread.Sleep(3000);
                    Console.WriteLine("Concurrent task is running....");
                }
            }, token);

            Console.WriteLine("Press any key to exit");

            Console.ReadKey();

            tokenSource.Cancel();

            task.Wait();

            Console.WriteLine("Task completed.");
        }

        private static void TestContinue() {
            const int MaxValue = 1000000000;

            watch.Restart();
            int numTasks = Environment.ProcessorCount;
            pendingTask = numTasks;
            int perThreadCount = MaxValue / numTasks;
            int perThreadLeftover = MaxValue % numTasks;

            var tasks = new List<Task<long>>(new Task<long>[numTasks]);
            // var tasks = new Task<long>[numTasks];

            for (int i = 0; i < numTasks; i++)
            {
                int start = i * perThreadCount;
                int end = (i + 1) * perThreadCount;
                if (i == numTasks - 1)
                {
                    end += perThreadLeftover;
                }
                tasks[i] = Task.Run(() => {
                    long threadSum = 0;
                    for (int j = start; j <= end; j++)
                    {
                        threadSum += (long)Math.Sqrt(j);
                    }
                    return threadSum;
                });
                tasks[i].ContinueWith(OnTaskEnd, TaskContinuationOptions.ExecuteSynchronously);
            }
            Task.WaitAll(tasks.ToArray());
        }

        private static void OnTaskEnd(Task<long> task)
        {
            Console.WriteLine("Thread sum: {0} ", task.Result);
            if (Interlocked.Decrement(ref pendingTask) == 0)
            {
                watch.Stop();
                Console.WriteLine("Task: {0} ", watch.Elapsed);
            }
        }
    }

    public static class ExceptionUtils
    {
        public static void LogException(System.Exception exception)
        {
            LogExceptionRecursive(exception, 0);
        }

        private static void LogExceptionRecursive(System.Exception ex, int recursionLevel) {
            if (ex == null)
            {
                return;
            }

            if (recursionLevel >= 10)
            {
                // Just to be safe, have a recursion cutoff
                return;
            }

            Console.WriteLine($"Type: {ex.GetType()}, Message: {ex.Message}, " + 
                $" Stack: {ex.StackTrace}, Level: {recursionLevel}"
                );

            var aggEx = ex as AggregateException;
            if (aggEx != null & aggEx?.InnerExceptions != null)
            {
                foreach (var inner in aggEx?.InnerExceptions)
                {
                    LogExceptionRecursive(inner, recursionLevel + 1);
                }
            }
            else if (ex.InnerException != null)
            {
                LogExceptionRecursive(ex.InnerException, recursionLevel + 1);
            }
        }
    }

}