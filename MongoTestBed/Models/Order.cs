using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoTestBed.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? ProductName { get; set; }

        public int ProductQuantity { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }

    public class OrderRequestModel 
    {
        public string? ProductName { get; set; }

        public int ProductQuantity { get; set; }
    }
}
