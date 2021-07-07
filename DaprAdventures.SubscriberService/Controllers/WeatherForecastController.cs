using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapr;
using Dapr.Client;
using DaprAdventures.SubscriberService.Models;

namespace DaprAdventures.SubscriberService.Controllers
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


        [Topic("pubsub", "dapreventhub")]
        [HttpPost("/receiver")]
        public async Task<IActionResult> Subscriber(WeatherChange weatherChange)
        {
            // Deserialize incoming order summary
            _logger.LogInformation("Received Weather: {@WeatherChange}", weatherChange);
            return Ok();
        }
    }
}
