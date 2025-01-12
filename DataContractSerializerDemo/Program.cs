using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

namespace DataContractSerializerDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            string xml = @"
            <Person xmlns='http://schemas.datacontract.org/2004/07/DataContractSerializerDemo'>
                <Age>30</Age>
                <Name>John Doe</Name>
                <Job>Engineer</Job>
            </Person>";

            // Create a DataContractSerializer for the Person type
            DataContractSerializer serializer = new DataContractSerializer(typeof(Person));

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                // Deserialize the XML into an object
                Person person = (Person)serializer.ReadObject(stream);

                // Output the deserialized object
                Console.WriteLine($"Age: {person.Age}");
                Console.WriteLine($"Name: {person.Name}");
                Console.WriteLine($"Job: {person.Job}"); 

            }
        }
    }

    [DataContract]
    public class Person
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public int Age { get; set; }
        [DataMember] public string Job { get; set; }
    }
}
