using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using static CitizenHackathon2025.DAL.Entities.WeatherForecast;

namespace CitizenHackathon2025.BLL.Interfaces
{
    public interface IWeatherService
    {
#nullable disable
        Task<IEnumerable<WeatherForecast?>> GetLatestWeatherForecastAsync();
        Task<WeatherForecast> SaveWeatherForecastAsync(WeatherForecast @weatherForecast);
    }
}
