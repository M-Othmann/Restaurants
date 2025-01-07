using Microsoft.AspNetCore.Mvc;

namespace Restaurants.Controllers
{
    public class Temprature
    {
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
    }



    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForcastService _weatherForcastService;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForcastService weatherForcastService)
        {
            _logger = logger;
            _weatherForcastService = weatherForcastService;
        }

        [HttpPost("Generate")]
        public IActionResult Generate([FromQuery] int count, [FromBody] Temprature request)
        {
            if (count > 0 && request.MaxTemp > request.MinTemp)
            {
                var result = _weatherForcastService.Get(count, request.MinTemp, request.MaxTemp);

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
