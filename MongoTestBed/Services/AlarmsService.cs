using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoTestBed.Models;
using MongoTestBed.Repositories;

namespace MongoTestBed.Services
{
    public class AlarmsService
    {

        private readonly AlarmsRepository _alarmsRepository;
        public AlarmsService(AlarmsRepository alarmsRepository)
        {
            _alarmsRepository = alarmsRepository;
        }

        public async Task<Alarm> CreateAlarmAsync(CreateAlarmRequestModel request) {
            Alarm newAlarm = new Alarm
            {
                AlarmId = Guid.NewGuid().ToString(),
                Category = request.Category,
                IsCompleted = request.IsCompleted,
                Histories = new List<AlarmHistory> { }
            };
            AlarmHistory newHistory = new AlarmHistory
            {
                HistoryId = Guid.NewGuid().ToString(),
                Severity = request.Severity,
                StartTime = DateTimeOffset.UtcNow,
                Description = request.Description
            };
            newAlarm.Histories.Add(newHistory);
            await _alarmsRepository.CreateAlarmAsync(newAlarm);
            return newAlarm;
        }

        public async Task UpdateAlarmAsync(string alarmId, string historyId, bool isCompleted, string newSeverity, string newDes) {
            await _alarmsRepository.UpdateAlarmAsync(alarmId, historyId, isCompleted, newSeverity, newDes);
        }
    }
}
