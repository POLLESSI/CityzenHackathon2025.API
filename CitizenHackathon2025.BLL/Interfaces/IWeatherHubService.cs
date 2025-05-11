
using CitizenHackathon2025.DAL.Entities;

namespace CitizenHackathon2025.BLL.Interfaces
{
    public interface IWeatherHubService
    {
        Task BroadcastWeatherAsync(WeatherForecast forecast, CancellationToken cancellationToken);
        Task SendWeatherToAllClientsAsync();
    }
}
