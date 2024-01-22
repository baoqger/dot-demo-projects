namespace ENum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChangeNotificationType a = ChangeNotificationType.CreateNotification;
            string text = $"p3.{a}";
            Console.WriteLine("Hello, World!" + text);
        }
    }

    public enum ChangeNotificationType
    {
        CreateNotification,
        UpdateNotification,
        DeleteNotification
    }
}

