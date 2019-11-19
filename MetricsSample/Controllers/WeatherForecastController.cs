using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenCensus.Trace;
using Steeltoe.Management.Census.Trace;

namespace MetricsSample.Controllers
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
        private readonly ITracing _tracing;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITracing tracing)
        {
            _logger = logger;
            _tracing = tracing;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            Thread.Sleep(50);
            using (_tracing.Tracer.SpanBuilder("Hi").StartScopedSpan())
            {
                Thread.Sleep(100);
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    })
                    .ToArray();
            }
        }
    }
}