using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;


namespace XMLSerializerDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Address a = new Address() { Street = "Street one", Postcode = "12345"};
            Person p = new Person() { Name = "Chris", Age = 30, HomeAddress = a};
            var ds = new DataContractSerializer(typeof(Person));
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true};
            using (XmlWriter x = XmlWriter.Create("person.xml", settings)) {
                ds.WriteObject(x, p);
            }
        }
    }

    [DataContract(Name = "Candidate")]
    public class Person {
        [DataMember] public string Name { get; set; }
        [DataMember] public int Age { get; set; }
        [DataMember] public Address HomeAddress { get; set; }
    }

    [DataContract]
    public class Address {
        [DataMember] public string Street, Postcode;
    }
}
