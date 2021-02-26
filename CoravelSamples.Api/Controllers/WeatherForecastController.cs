using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Queuing.Interfaces;
using CoravelSamples.Api.Invocables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoravelSamples.Api.Controllers
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
        private readonly IQueue _queue;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IQueue queue)
        {
            _logger = logger;
            _queue = queue;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Queueing very long task...");
            _queue.QueueInvocable<VeryLongTaskInvocable>();

            //This controller will still respond in less than one second and the very long task will be run in background.

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
