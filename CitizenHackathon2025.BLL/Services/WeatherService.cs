using Microsoft.AspNetCore.SignalR.Client;
using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.BLL;
using CitizenHackathon2025.DAL.Repositories;
using CitizenHackathon2025.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Components;
using CitizenHackathon2025.DAL.Entities;
using Microsoft.Extensions.Logging;

namespace CitizenHackathon2025.BLL.Services
{
    public class WeatherService : IWeatherService
    {
    #nullable disable
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<IEnumerable<WeatherForecast?>> GetLatestWeatherForecastAsync()
        {
            var weatherForecasts = await _weatherRepository.GetLatestWeatherForecastAsync();
            return null;
        }

        public async Task<WeatherForecast> SaveWeatherForecastAsync(WeatherForecast weatherForecast)
        {
            return await _weatherRepository.SaveWeatherForecastAsync(weatherForecast);
        }
    }
}
