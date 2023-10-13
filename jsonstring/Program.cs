using System;
using System.Xml;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        // Define a sample object
        Person person = new Person
        {
            Name = "Alice",
            Age = 25,
            Email = "alice@example.com"
        };

        // Serialize the object to a JSON string
        string json = JsonConvert.SerializeObject(person);
        var d = DateTimeOffset.Now.Ticks;
        // Print the JSON string
        Console.WriteLine(d);
    }
}

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}