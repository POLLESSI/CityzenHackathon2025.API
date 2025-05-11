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
    public class WeatherForecastService : IWeatherForecastService
    {
    #nullable disable
        private readonly IWeatherForecastRepository _weatherRepository;
        private readonly Random _rng = new();


        public WeatherForecastService(IWeatherForecastRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<WeatherForecast> GenerateNewForecastAsync()
        {
            var forecast = new WeatherForecast
            {
                DateWeather = DateTime.Now,
                TemperatureC = "_rng.Next(-10, 35)",
                Summary = "Généré",
                RainfallMm = "Math.Round(_rng.NextDouble() * 20, 1)",
                Humidity = "_rng.Next(30, 100)",
                WindSpeedKmh = "Math.Round(_rng.NextDouble() * 80, 1)"
            };

            await _weatherRepository.SaveWeatherForecastAsync(forecast);
            return forecast;
        }

        public Task<List<WeatherForecast>> GetHistoryAsync()
        {
            return _weatherRepository.GetAllAsync();
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
