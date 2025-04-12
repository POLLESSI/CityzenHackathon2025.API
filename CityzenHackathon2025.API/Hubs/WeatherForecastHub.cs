using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CityzenHackathon2025.API.Hubs
{
    public class WeatherForecastHub : Hub
    {
#nullable disable

        private readonly ILogger<WeatherForecastHub> _logger;

        public WeatherForecastHub(ILogger<WeatherForecastHub> logger)
        {
            _logger = logger;
        }

        public async Task RefreshWeatherForecast()
        {
            _logger.LogInformation("NotifyNewWeatherForecast called");
            await Clients.All.SendAsync("NewWeatherForecast");
        }
    }
}
