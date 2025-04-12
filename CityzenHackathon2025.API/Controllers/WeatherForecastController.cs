using System.Numerics;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.DAL.Interfaces;
using CitizenHackathon2025.DAL.Repositories;
using CityzenHackathon2025.API.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CityzenHackathon2025.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly IHubContext<WeatherForecastHub> _hubContext;

    public WeatherForecastController(IWeatherRepository weatherRepository, IHubContext<WeatherForecastHub> hubContext)
    {
        _weatherRepository = weatherRepository;
        _hubContext = hubContext;
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestWeatherForecast()
    {
        var forecast = await _weatherRepository.GetLatestWeatherForecastAsync();
        if (forecast == null)
        {
            return NotFound();
        }
        return Ok(forecast);
    }
    //[HttpPost]
    //public async Task<IActionResult> SaveWeatherForecast([FromBody] WeatherForecast forecast)
    //{
    //    //if (!ModelState.IsValid)
    //    //    return BadRequest(ModelState);
    //    //var savedForecast = await _weatherRepository.SaveWeatherForecastAsync(forecast);

    //    //if (savedForecast == null)
    //    //    return StatusCode(500, "Erreur lors de l'enregistrement");

    //    // Broadcast clients SignalR
    //    //await _hubContext.Clients.All.SendAsync("NewWeatherForecast", savedForecast);

    //    return Ok(savedForecast);
    //}
}
