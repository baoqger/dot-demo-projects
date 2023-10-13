using System.Text;
using System.Xml.Linq;

namespace XMLDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XElement bookElement = new XElement((XName)"Response", new XElement((XName)"Success", 0));
            
            // Convert the XElement to XML string
            string xmlString = bookElement.ToString();
            var response = Encoding.UTF8.GetBytes(xmlString);
            Console.WriteLine(xmlString);
            Console.WriteLine(response.Length);
        }
    }
}