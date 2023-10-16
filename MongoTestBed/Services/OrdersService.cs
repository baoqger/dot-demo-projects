using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoTestBed.Models;

namespace MongoTestBed.Services
{
    public class OrdersService
    {
        private readonly IMongoCollection<Order> _ordersCollection;

        public OrdersService(
            IOptions<OrderStoreDatabaseSettings> orderStoreDatabaseSettings
        )
        {
            var mongoClient = new MongoClient(
                orderStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                orderStoreDatabaseSettings.Value.DatabaseName);

            _ordersCollection = mongoDatabase.GetCollection<Order>(
                orderStoreDatabaseSettings.Value.BooksCollectionName);

        }

        public async Task CreateOrderAsync(Order newOrder)
        {
            await _ordersCollection.InsertOneAsync(newOrder);
        }

        public async Task<List<Order>> GetOrderAsync() {
            var filter = Builders<Order>.Filter.Empty;
            return await _ordersCollection.Find(filter).ToListAsync();
        }
    }
}
