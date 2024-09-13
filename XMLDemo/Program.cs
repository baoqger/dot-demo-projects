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
            ReadXMLString();
            ReadDrillingProgramIndexFile();
            Console.ReadLine();
        }

        static void ReadDrillingProgramIndexFile() {

            // Your XML string
            string xmlString = 
        @"<?xml version='1.0' encoding='utf-8'?> 
        <drillingProgram xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' 
                         xmlns:xsd='http://www.w3.org/2001/XMLSchema' 
                         uid='9717dd32-7f91-49cb-aa5d-71e73194ef08' 
                         schemaVersionMajor='11' 
                         schemaVersionMinor='0' 
                         witsmlVersion='1.4.1.1' 
                         producer='DrillPlan' 
                         dataPartitionId='drillplan-bgc-drillplan-bgc-bi' 
                         revision='41' 
                         xmlns='http://www.slb.com/schema/drillingprogram'>
            <well uid='b7fcc27f-d701-4272-84fe-026a13ce79a6' file='022_Well.xml'>oss_test</well>
            <wellbores>
                <wellbore uid='af7d465b-40d8-4947-90ce-dc5d1af06d3a' file='019_Wellbore.xml'>Wellbore 1</wellbore>
                <wellbore uid='28e2df15-f88d-4428-b975-be22eae3573a' file='020_Wellbore.xml'>wellbore 2.1</wellbore>
                <wellbore uid='86f744dd-5f88-481c-ba4e-7994a975d49e' file='021_Wellbore.xml'>wellbore 3</wellbore>
            </wellbores>
            <rigs>
                <rig uid='603d8c54-593e-446b-81ab-e1241b86f92e' file='009_Rig.xml' uidWellbore='af7d465b-40d8-4947-90ce-dc5d1af06d3a'>Rig 1 update for multi-wellbore</rig>
                <rig uid='994f2ad3-e392-491a-96ac-f5b13d55b131' file='009_Rig.xml' uidWellbore='af7d465b-40d8-4947-90ce-dc5d1af06d3a'>Rig 2</rig>
            </rigs>
            <anticollisionBlobs>
                <anticollisionBlob file='blobs/AC_Blob_849a6d4f8fe24629a059c76d3b9d3c73' version='2023.11.0.3' uidWellbore='849a6d4f-8fe2-4629-a059-c76d3b9d3c73' />
            </anticollisionBlobs>
            <dataReferences>
                <dataRef file='009_TrajectoryInterpolated_v1.xml' artifactType='TrajectoryInterpolated_v1_witsml141' uid='b633695450d754909159a6f234ddb3a1' uidWellbore='28f02ec8-dc18-474b-8110-52019037b2f3' />
                <dataRef file='010_TrajectoryInterpolated_v1.xml' artifactType='TrajectoryInterpolated_v1_witsml141' uid='5fe4cc747d5c5e15bc1bafa610423e87' uidWellbore='849a6d4f-8fe2-4629-a059-c76d3b9d3c73' />
                <dataRef file='011_DrillingFluid_v5_witsml141.xsd' artifactType='DrillingFluid_v5_witsml141_xsd' uid='9dc885559b6743209a9177ee6e7c997c' />
                <dataRef file='012_Well_v3_witsml141.xml' artifactType='Well_v3_witsml141' uid='9dc885559b6743209a9177ee6e7c997c' />
                <dataRef file='013_ArtifactIndex.json' artifactType='ArtifactIndex' uid='9dc885559b6743209a9177ee6e7c997c' />
                <dataRef file='014_Cementing_v3_witsml141.xsd' artifactType='Cementing_v3_witsml141_xsd' uid='9dc885559b6743209a9177ee6e7c997c' />
                <dataRef file='016_ToleranceTunnel.json' artifactType='ToleranceTunnel' uid='27b3f12c12175f49b94f6dd1bb978942' uidWellbore='849a6d4f-8fe2-4629-a059-c76d3b9d3c73' />
            </dataReferences>
        </drillingProgram>";

            // Parse the XML string
            XElement xmlElement = XElement.Parse(xmlString);

            // Use LINQ to XML to get the uid attribute value of the specified dataRef element
            var dataRefElement = xmlElement.Descendants(XName.Get("dataRef", "http://www.slb.com/schema/drillingprogram"))
                                            .FirstOrDefault(d => (string)d.Attribute("artifactType") == "ToleranceTunnel");

            // Check if the element was found and retrieve the uid attribute
            if (dataRefElement != null)
            {
                string uid = dataRefElement.Attribute("uid")?.Value;
                Console.WriteLine($"UID of ToleranceTunnel file '016_ToleranceTunnel.json': {uid}");
            }
            else
            {
                Console.WriteLine("Element not found.");
            }
        }

        static void ReadXMLString() {
            // Your XML string
            string xmlString = 
        @"<?xml version='1.0' encoding='utf-8'?>
        <library>
            <book>
                <title>Programming in C#</title>
                <author>John Doe</author>
                <price>29.99</price>
            </book>
            <book>
                <title>Learning Python</title>
                <author>Jane Smith</author>
                <price>34.99</price>
            </book>
        </library>";

            // Parse the XML string into an XElement
            XElement xmlElement = XElement.Parse(xmlString);

            // Iterate through each 'book' element
            foreach (var book in xmlElement.Elements("book"))
            {
                string title = book.Element("title")?.Value;
                string author = book.Element("author")?.Value;
                string price = book.Element("price")?.Value;

                // Print the book details
                Console.WriteLine($"Title: {title}");
                Console.WriteLine($"Author: {author}");
                Console.WriteLine($"Price: {price}");
                Console.WriteLine();
            }
        }
    }
}