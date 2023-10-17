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
        public async Task<Alarm> Create([FromBody] CreateAlarmRequestModel request)
        {
            return await _alarmsService.CreateAlarmAsync(request);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAlarmRequestModel request)
        {
            await _alarmsService.UpdateAlarmAsync(request.AlarmId, request.HistoryId, request.IsCompleted, request.Severity, request.Description);
            return CreatedAtAction(nameof(Update), new { id = request.AlarmId }, null);
        }

    }
}
