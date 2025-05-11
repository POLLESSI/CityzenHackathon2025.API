using System.Numerics;
using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.BLL.Services;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.DAL.Interfaces;
using CitizenHackathon2025.DAL.Repositories;
using CityzenHackathon2025.API.DTOs;
using CityzenHackathon2025.API.Hubs;
using CityzenHackathon2025.API.Tools;
using CityzenHackathon2025.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CityzenHackathon2025.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastRepository _weatherRepository;
    private readonly IHubContext<WeatherForecastHub> _hubContext;
    private readonly IOpenWeatherMapService _owmService;

    public WeatherForecastController(IWeatherForecastRepository weatherRepository, IHubContext<WeatherForecastHub> hubContext, IOpenWeatherMapService owmService)
    {
        _weatherRepository = weatherRepository;
        _hubContext = hubContext;
        _owmService = owmService;
    }
    [HttpGet("openweather")]
    public async Task<IActionResult> GetForecastFromOpenWeather()
    {
        var externalDto = await _owmService.GetForecastAsync("Namur");
        if (externalDto == null)
            return NotFound();

        // DTO → Entity → DTO (unification de mapping)
        var entity = externalDto.MapToWeatherForecast();
        var apiDto = entity.MapToWeatherForecastDTO();

        await _hubContext.Clients.All.SendAsync("ExternalWeatherUpdate", apiDto);
        return Ok(apiDto);
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestWeatherForecast()
    {
        var model = await _weatherRepository.GetLatestWeatherForecastAsync();
        if (model == null)
            return NotFound();

        var forecastDto = model.MapToWeatherForecastDTO(); // Utilisation de ton Mappers
        return Ok(forecastDto);
    }
    [HttpGet("current")]
    public async Task<ActionResult<WeatherForecast>> GetCurrentWeather()
    {
        var forecast = await _weatherRepository.GenerateNewForecastAsync();
        await _hubContext.Clients.All.SendAsync("ReceiveWeather", forecast);
        return Ok(forecast);
    }
    [HttpGet("history")]
    public async Task<ActionResult<List<WeatherForecast>>> GetHistory()
    {
        return Ok(await _weatherRepository.GetHistoryAsync());
    }

    [HttpPost]
    public async Task<IActionResult> SaveWeatherForecast([FromBody] WeatherForecastDTO forecastDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var forecast = forecastDto.MapToWeatherForecast(); 

        var savedForecast = await _weatherRepository.SaveWeatherForecastAsync(forecast);

        if (savedForecast == null)
            return StatusCode(500, "Registration Error");

        var forecastDtoToSend = savedForecast.MapToWeatherForecastDTO();

        await _hubContext.Clients.All.SendAsync("NewWeatherForecast", forecastDtoToSend);

        return Ok(forecastDtoToSend);
    }
}
