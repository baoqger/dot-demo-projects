using Microsoft.AspNetCore.Mvc;
using MongoTestBed.Models;
using MongoTestBed.Services;

namespace MongoTestBed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly OrdersService _ordersService;

        public OrderController(OrdersService ordersService) {
            _ordersService = ordersService;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderRequestModel request)
        {
            Order newOrder = new Order { 
                ProductName = request.ProductName,
                ProductQuantity = request.ProductQuantity,
                CreateTime = DateTimeOffset.UtcNow
            };
            await _ordersService.CreateOrderAsync(newOrder);

            return CreatedAtAction(nameof(Create), new { id = newOrder.Id }, newOrder);
        }

        [HttpGet]
        public async Task<List<Order>> Get() {
            return await _ordersService.GetOrderAsync();
        }
    }
}