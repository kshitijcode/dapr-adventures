using Newtonsoft.Json;

namespace DaprAdventures.PublisherService.Models
{
    public class WeatherChange
    {
        public string oldWeatherState { get; set; }
        public string currentWeatherState { get; set; }
    }
}