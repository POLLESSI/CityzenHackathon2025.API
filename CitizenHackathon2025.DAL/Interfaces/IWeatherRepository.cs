using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using static CitizenHackathon2025.DAL.Entities.WeatherForecast;

namespace CitizenHackathon2025.DAL.Interfaces
{
    public interface IWeatherRepository
    {
        Task<WeatherForecast?> GetLatestWeatherForecastAsync();
        Task<WeatherForecast> SaveWeatherForecastAsync(WeatherForecast forecast);
    }
}
