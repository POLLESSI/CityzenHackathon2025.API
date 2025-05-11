using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.DAL.Entities;
using Microsoft.AspNetCore.SignalR;
using CitizenHackathon2025.API.Hubs;
using CityzenHackathon2025.API.Hubs;

namespace CitizenHackathon2025.BLL.Services
{
    public class WeatherHubService : IWeatherHubService
    {
        private readonly IHubContext<WeatherForecastHub> _hubContext;

        public WeatherHubService(IHubContext<WeatherForecastHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastWeatherAsync(WeatherForecast forecast, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveWeather", forecast, cancellationToken);
        }

        public Task SendWeatherToAllClientsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
