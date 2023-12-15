using Azure.Core.Serialization;
using Azure.Messaging.ServiceBus;
using Azure_Service_Bus_Queue_Example.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Azure_Service_Bus_Queue_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        IServiceBus serviceBus;
        
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IServiceBus serviceBus)
        {
            _logger = logger;
            this.serviceBus = serviceBus;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task Post(WeatherForecast weatherForecast)
        {
            //add-weather-data is a que name given in the azure cloud
            var sender = serviceBus.GetServiceBusClient().CreateSender("add-weather-data");
            var body = JsonSerializer.Serialize(weatherForecast);
            var message = new ServiceBusMessage(body);

            if (body.Contains("ttl"))
            {   // Set time to live to delete the message if not peeked up and processed
                message.TimeToLive = TimeSpan.FromSeconds(1);
            }
            await sender.SendMessageAsync(message);
            
        }
    }
}