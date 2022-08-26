using DogApi.Managers;
using DogApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogApi.Controllers
{
    [ApiController]
    [Route("api")]
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

        [HttpGet("Dog")]
        public Dog Get()
        {
            DogManager manager = new DogManager();
            return manager.GetDog(manager.GetBreed());
        }
    }
}