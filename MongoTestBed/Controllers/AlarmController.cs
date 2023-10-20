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
        [HttpGet]
        public async Task<List<Alarm>> Get() 
        {
            return await _alarmsService.GetAlarmsAsync();
        }

        [HttpGet, Route("test")]
        public async Task<Order> TestGet(string id, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null) {
            var o = new Order {
                OrderId = Guid.NewGuid().ToString(),
                ProductName = "test",
                ProductQuantity = 1,
                CreateTime = DateTimeOffset.UtcNow
            };
            return o;
        }

    }
}
