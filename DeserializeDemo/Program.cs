using Slb.Prism.Shared.Library.Protocol;
using Slb.Prism.Shared.Library.Protocol.V3;
using Version = Slb.Prism.Shared.Library.Protocol.Version;
using Avro;
using Energistics.Datatypes;

namespace DeserializeDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var p = new Person() {
                Name = "Chris",
                Age = 1,
            };

            var c = new Company()
            {
                Name = "Goo",
                Country = "US"
            };
            var e = new Employee() {
                Person = p,
                Company = c,
            };


            var prismheader = new PrismHeader()
            {
                ContainerId = "containerId",
                MessageId = new Uuid(),
                SessionId = new Uuid(),
                Source = "source",
                ContentType = "contentType",
                Version = Version.V3,
            };

            var messageheader = new MessageHeader()
            {
                CorrelationId = 3L,
                MessageId = 1000L,
                MessageType = 1,
                //4 means put context message.
                Protocol = 1,
                MessageFlags = 1
            };

            byte[] data;
            using (var stream = new MemoryStream()) {
                var ds = new DataSerializer();

                ds.Serialize(prismheader, stream);
                ds.Serialize(messageheader, stream);
                data = stream.ToArray();
            }

            Console.WriteLine("debug byte array: " + data.Length);

            using (var stream = new MemoryStream(data)) {
                var ds2 = new DataSerializer();
                var h = ds2.Deserialize<PrismHeader>(stream);
                Console.WriteLine("debug deserialize: " + h.Source);

                var m = ds2.Deserialize<MessageHeader>(stream);
                Console.WriteLine("debug deserialize: " + m.CorrelationId);

            }

            Console.ReadLine();

        }
    }


    public class EnhancedPrismHeader: PrismHeader {
        public Employee Employee { get; set; }
    }

    public class Employee { 
        public Person Person { get; set; }
        public Company Company { get; set; }
    }

    public class Person { 
        public string Name { get; set; }
        public int Age { get; set; }

        public static Schema _SCHEMA = Schema.Parse("{\"type\":\"record\",\"name\":\"PrismHeader\",\"namespace\":\"Slb.Prism.Shared.Library.Protocol.V3\",\"fields\":[{\"name\":\"version\",\"doc\":\"[Mandatory] Version of the Prism message schema used to implement the message.\",\"type\":{\"type\":\"record\",\"name\":\"Version\",\"namespace\":\"Slb.Prism.Shared.Library.Protocol\",\"fields\":[{\"name\":\"major\",\"doc\":\"Major version needs to be incremented when incompatible schema changes are made.\",\"type\":\"int\"},{\"name\":\"minor\",\"doc\":\"Minor version needs to be incremented when backward compatible schema enhancement are made.\",\"type\":\"int\"},{\"name\":\"revision\",\"doc\":\"Not use. Always set to 0 at this stage.\",\"type\":\"int\"},{\"name\":\"patch\",\"doc\":\"Patch version needs to be incremented when backwards-compatible bug fixes, typo...\",\"type\":\"int\"}],\"stability\":\"frozen\"}},{\"name\":\"messageId\",\"doc\":\"[Mandatory] Unique identifier of the message.\",\"type\":{\"type\":\"fixed\",\"name\":\"Uuid\",\"namespace\":\"Slb.Prism.Shared.Library.Protocol\",\"size\":16,\"stability\":\"stable\",\"reference\":\"https://issues.apache.org/jira/browse/AVRO-1962\"}},{\"name\":\"sessionId\",\"doc\":\"[Mandatory] The open ETP session under which all messages (specified as ETP or not) are transported.\",\"type\":\"Slb.Prism.Shared.Library.Protocol.Uuid\"},{\"name\":\"containerId\",\"doc\":\"[Mandatory] Identifier of the container which the transported data belongs to.\",\"type\":\"string\"},{\"name\":\"source\",\"doc\":\"[Mandatory] Name of the system producer of teh message.\",\"type\":\"string\"},{\"name\":\"contentType\",\"doc\":\"[Mandatory] Follows RFC1341 describing the content of the message.\",\"type\":\"string\"}],\"fullName\":\"Slb.Prism.Shared.Library.Protocol.V3.PrismHeader\",\"depends\":[\r\n  \"Slb.Prism.Shared.Library.Protocol.Version\",\r\n  \"Slb.Prism.Shared.Library.Protocol.Uuid\"\r\n]}");
    }

    public class Company 
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
