using System;
using System.Xml;
using System.Text.Json;
using System.Text;

class Program
{
    static void Main()
    {
        // Sample JSON string
        string jsonString = "{\"name\": \"John\", \"age\": 30}";

        // Convert JSON string to byte array
        byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);

        Console.ReadLine();

        string jsonString2   = Encoding.UTF8.GetString(byteArray);

        Console.WriteLine(jsonString);
        // Now byteArray contains the byte representation of the JSON string
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