using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoTestBed.Models;

namespace MongoTestBed.Repositories
{
    public class AlarmsRepository
    {
        private readonly IMongoCollection<Alarm> _alarmsCollection;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public AlarmsRepository(IOptions<AlarmStoreDatabaseSettings> alarmStoreDatabaseSettings) {
            _mongoClient = new MongoClient(alarmStoreDatabaseSettings.Value.ConnectionString);

            _mongoDatabase = _mongoClient.GetDatabase(alarmStoreDatabaseSettings.Value.DatabaseName);

            _alarmsCollection = _mongoDatabase.GetCollection<Alarm>(alarmStoreDatabaseSettings.Value.AlarmsCollectionName);
        }

        public async Task CreateAlarmAsync(Alarm newAlarm)
        {
            await _alarmsCollection.InsertOneAsync(newAlarm);
        }

        public async Task UpdateAlarmAsync(string alarmId, string historyId, bool isCompleted, string newSeverity, string newDes) {
            await _lock.WaitAsync();
            try {
                // validate first
                var alarmFilter = Builders<Alarm>.Filter.Eq(a => a.AlarmId, alarmId);
                var alarm = await _alarmsCollection.Find<Alarm>(alarmFilter).FirstOrDefaultAsync();
                var histories = alarm.Histories;
                var history = histories?.Find((h) => h.HistoryId == historyId);
                if (alarm.IsCompleted || history?.EndTime != DateTimeOffset.MinValue)
                {
                    return;
                }

                history.EndTime = DateTimeOffset.UtcNow;
                var newHistory = new AlarmHistory
                {
                    HistoryId = Guid.NewGuid().ToString(),
                    Severity = newSeverity,
                    Description = newDes,
                    StartTime = DateTimeOffset.UtcNow,
                };
                histories?.Add(newHistory);

                var update = Builders<Alarm>.Update
                                .Set(a => a.IsCompleted, isCompleted)
                                .Set(a => a.Histories, histories);

                await _alarmsCollection.UpdateOneAsync(alarmFilter, update);

            } finally
            {
                _lock.Release();
            }

        }

        public async Task UpdateAlarmWithTransactionAsync(string alarmId, string historyId, bool isCompleted, string newSeverity, string newDes)
        {
            using (var session = await _mongoClient.StartSessionAsync()) {

                session.StartTransaction();

                try
                {
                    var alarmFilter = Builders<Alarm>.Filter.Eq(a => a.AlarmId, alarmId);
                    var historyFilter = Builders<Alarm>.Filter.ElemMatch(a => a.Histories, h => h.HistoryId == historyId);

                    var filter = Builders<Alarm>.Filter.And(alarmFilter, historyFilter);


                    var update = Builders<Alarm>.Update
                            .Set(a => a.IsCompleted, isCompleted)
                            .Set(a => a.Histories.FirstMatchingElement().EndTime, DateTimeOffset.UtcNow);

                    await _alarmsCollection.UpdateOneAsync(filter, update);

                    var newHistory = new AlarmHistory
                    {
                        HistoryId = Guid.NewGuid().ToString(),
                        Severity = newSeverity,
                        Description = newDes,
                        StartTime = DateTimeOffset.UtcNow,
                    };
                    var push = Builders<Alarm>.Update.Push(a => a.Histories, newHistory);
                    await _alarmsCollection.UpdateOneAsync(alarmFilter, push);

                    await session.CommitTransactionAsync();

                } 
                catch(Exception ex)
                {
                    await session.AbortTransactionAsync();
                }

            }

        }
    }
}
