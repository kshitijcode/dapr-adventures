using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DaprAdventures.PublisherService.Models;
using Dapr.Client;

namespace DaprAdventures.PublisherService.Controllers
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
        private readonly DaprClient _daprClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        
        [HttpPost]
        public async Task<IActionResult> WeatherChange([FromBody] WeatherChange weatherChange)
        {

            _logger.LogInformation("Customer Order received: {@WeatherChange}", weatherChange);

            try
            {
                await _daprClient.PublishEventAsync<WeatherChange>("pubsub", "dapreventhub", weatherChange);
            }
            catch (Exception e)
            {
                return BadRequest("Please try again");
            }

            return Ok("Weather Change Published");

        }


    }
}
