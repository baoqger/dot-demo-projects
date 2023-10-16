using Microsoft.AspNetCore.Mvc;
using MongoTestBed.Models;
using MongoTestBed.Services;

namespace MongoTestBed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlarmController : ControllerBase
    {
        private readonly AlarmsService _alarmsService;

        public AlarmController(AlarmsService alarmsService)
        {
            _alarmsService = alarmsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAlarmRequestModel request)
        {
            Alarm newAlarm = new Alarm
            {
                AlarmId = Guid.NewGuid().ToString(),
                Category = request.Category,
                IsCompleted = request.IsCompleted,
                Histories = new List<AlarmHistory> {}
            };
            AlarmHistory newHistory = new AlarmHistory
            {
                HistoryId = Guid.NewGuid().ToString(),
                Severity = request.Severity,
                StartTime = DateTimeOffset.UtcNow,
                Description= request.Description
            };
            newAlarm.Histories.Add(newHistory);
            await _alarmsService.CreateAlarmAsync(newAlarm);

            return CreatedAtAction(nameof(Create), new { id = newAlarm.AlarmId }, newAlarm);
        }


    }
}
