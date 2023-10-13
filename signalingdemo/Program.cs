namespace signalingdemo
{
    internal class Program
    {
        static EventWaitHandle _waitHandle = new AutoResetEvent(false);
        static void Main()
        {
            _waitHandle.Set(); // Wake up the Waiter.
            new Thread(Waiter).Start();
            Thread.Sleep(5000); // Pause for a second...
            
        }

        static void Waiter()
        {
            Console.WriteLine("Waiting...");
            _waitHandle.WaitOne(); // Wait for notification
            Console.WriteLine("Notified");
        }
    }
}