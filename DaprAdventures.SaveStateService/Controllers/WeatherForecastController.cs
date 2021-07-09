using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapr.Client;
using DaprAdventures.SaveStateService.Models;

namespace DaprAdventures.SaveStateService.Controllers
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

        [HttpPost]
        [Route("/saveWeatherState")]
        public async Task<IActionResult> Post([FromBody] WeatherChange weatherChange)
        {

            try
            {
                await _daprClient.SaveStateAsync("mongo", "admin", weatherChange);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();

        }
    }
}
