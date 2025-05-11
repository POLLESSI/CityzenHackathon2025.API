
using CityzenHackathon2025.Shared.DTOs;

namespace CitizenHackathon2025.BLL.Interfaces
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherForecastDTO?> GetForecastAsync(string city);
    }
}
