using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoTestBed.Models;

namespace MongoTestBed.Services
{
    public class AlarmsService
    {
        private readonly IMongoCollection<Alarm> _alarmsCollection;

        public AlarmsService(
    IOptions<AlarmStoreDatabaseSettings> alarmStoreDatabaseSettings
)
        {
            var mongoClient = new MongoClient(
                alarmStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                alarmStoreDatabaseSettings.Value.DatabaseName);

            _alarmsCollection = mongoDatabase.GetCollection<Alarm>(
                alarmStoreDatabaseSettings.Value.AlarmsCollectionName);

        }

        public async Task CreateAlarmAsync(Alarm newAlarm) {
            await _alarmsCollection.InsertOneAsync(newAlarm);
        }

        public async Task UpdateAlarmAsync(string alarmId, string historyId, string isCompleted, string newSeverity, string newDes) {
            var filter = Builders<Alarm>.Filter.And(
                    Builders<Alarm>.Filter.Eq(a => a.AlarmId, alarmId),
                    Builders<Alarm>.Filter.ElemMatch(a => a.Histories, h => h.HistoryId == historyId)
                );
            var update = Builders<Alarm>.Update
                            .Set(a => a.Histories.FirstMatchingElement().EndTime, DateTimeOffset.UtcNow);
            await _alarmsCollection.UpdateOneAsync(filter, update);
        }
    }
}
