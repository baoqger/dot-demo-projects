using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using Newtonsoft.Json;

namespace GenericAction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var test = "test string";
            Task.Run(async () => {
                await MigrateWorkflow(test);
            });
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        private async Task MigrateWorkflow(string a) {
            await Task.Delay(5000);
            Console.WriteLine("debug workflow..." + a);
        } 

        /// <summary>
        /// This is my action method.
        /// </summary>
        /// <typeparam name="T">The type of the request body.</typeparam>
        /// <param name="requestBody">The request body.</param>
        /// <returns>A string message.</returns>
        [HttpPost, Route("test")]
        public string MyAction([FromBody] FrictionFactor requestBody)
        {
            string message = JsonConvert.SerializeObject(requestBody);
            Console.WriteLine(message);
            return message;
        }
    }

    public class FrictionFactor
    {
        public double[] CasingHole { get; set; }
        public double[] OpenHole { get; set; }
    }
}