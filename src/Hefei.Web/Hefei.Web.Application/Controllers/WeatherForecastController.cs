using Microsoft.AspNetCore.Mvc;

namespace Hefei.Web.Application.Controllers;

[ApiController]
[Route(
    "[controller]"
)]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] s_summaries =
    {
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
        => _logger = logger;

    [HttpGet]
    public IEnumerable<WeatherForecast> Get() => Enumerable.Range(
            1,
            5
        )
        .Select(
            index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(
                    DateTime.Now.AddDays(
                        index
                    )
                ),
                TemperatureC = Random.Shared.Next(
                    -20,
                    55
                ),
                Summary = s_summaries[Random.Shared.Next(
                    s_summaries.Length
                )]
            }
        )
        .ToArray();
}
