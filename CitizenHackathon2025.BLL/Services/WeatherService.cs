using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.DAL.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CitizenHackathon2025.BLL.Services
{
    public class WeatherService : BackgroundService, IHostedService
    {
        private readonly IWeatherHubService _hubService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<WeatherService> _logger;
        private readonly string[] _summaries = new[] {
        "Sunny", "Cloudy", "Rainy", "Stormy", "Snowy", "Foggy"
    };
        private readonly Random _rng = new();

        public WeatherService(IWeatherHubService hubService, IServiceScopeFactory scopeFactory, ILogger<WeatherService> logger)
        {
            _hubService = hubService;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var forecast = new WeatherForecast
                {
                    DateWeather = DateTime.Now,
                    TemperatureC = "_rng.Next(-20, 55),",
                    Summary = _summaries[_rng.Next(_summaries.Length)],
                    RainfallMm = "Math.Round(_rng.NextDouble() * 20, 1)", // 0 à 20 mm
                    Humidity = "_rng.Next(30, 100)", // 30 à 100 %
                    WindSpeedKmh = "Math.Round(_rng.NextDouble() * 100, 1)"  // 0 à 100 km/h
                };
                _logger.LogInformation("Sending weather update: {@forecast}", forecast);
                await _hubService.BroadcastWeatherAsync(forecast, stoppingToken);
                await Task.Delay(5000, stoppingToken);
            };
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var hubService = scope.ServiceProvider.GetRequiredService<IWeatherHubService>();

            await hubService.SendWeatherToAllClientsAsync(); // ou autre logique
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
