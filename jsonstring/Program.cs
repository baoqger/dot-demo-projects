using System;
using System.Xml;
using System.Text.Json;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        var person1 = new Person { Name = "Alice", Age = 30, Email = "test", Hobbies = new List<string> { "Reading", "Hiking" } };
        var person2 = new Person { Name = "Bob", Age = 25, Email = "test2", Hobbies = new List<string> { "Gaming", "Cooking" } };   
        var person3 = new Person { Name = "Charlie", Age = 35, Email = "test3", Hobbies = new List<string> { "Traveling", "Photography" } };
        var group = new List<Person> { person1, person2, person3 }; 
        var json = JsonConvert.SerializeObject(group);
        Console.WriteLine("Serialized JSON:" + json);

        List<NewPerson> c = JsonConvert.DeserializeObject<List<NewPerson>>(json);

        Console.WriteLine("Deserialized Company Name: ");
    }

    static public void PrintPerson(params Person[] persons) {
        for (int i = 0; i < persons.Length; i++) {
            Console.WriteLine(persons[i].Name);
        }
    }
}

class Company { 
    public string Name { get; set; }
    public List<Person> Employees { get; set; }
}


class NewPerson: Person
{
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public List<string> Hobbies { get; set; }
}