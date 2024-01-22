using System;
using System.Xml;
using System.Text.Json;

class Program
{
    static void Main()
    {
        // Define a sample object
        Person p1 = new Person
        {
            Name = "Alice",
            Age = 25,
            Email = "alice@example.com",
            Hobbies = new List<string>()
        };

        Person p2 = new Person
        {
            Name = "Bob",
            Age = 25,
            Email = "alice@example.com",
            Hobbies = new List<string>()
};
        
        var persons = new List<Person> { p1, p2 };
        Console.WriteLine(JsonSerializer.Serialize(persons));
        persons.ForEach(person => { 
            person.Age = 28;
            person.Hobbies.Add("football");
        });
        Console.WriteLine(JsonSerializer.Serialize(persons));
        
        PrintPerson(persons.ToArray());
    }

    static public void PrintPerson(params Person[] persons) {
        for (int i = 0; i < persons.Length; i++) {
            Console.WriteLine(persons[i].Name);
        }
    }
}

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public List<string> Hobbies { get; set; }
}