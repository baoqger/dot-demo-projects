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

        public async Task<List<Alarm>> GetAlarmsAsync() { 
            return await _alarmsRepository.GetAlarmsAsync();
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
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Description = request.Description
            };
            newAlarm.Histories.Add(newHistory);
            await _alarmsRepository.CreateAlarmAsync(newAlarm);
            return newAlarm;
        }

        public async Task UpdateAlarmAsync(string alarmId, string historyId, bool isCompleted, string newSeverity, string newDes) {
            await _alarmsRepository.UpdateAlarmAsync(alarmId, historyId, isCompleted, newSeverity, newDes);
        }

        public void Debug(string id, DateTimeOffset? start, DateTimeOffset? end)
        {
            if (!start.HasValue) start = DateTimeOffset.MinValue;
            if (!end.HasValue) end = DateTimeOffset.MaxValue;
            Console.WriteLine("debug xxxxx", start.Value);
        }
    }
}
