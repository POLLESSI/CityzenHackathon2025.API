using System.Numerics;
using CitizenHackathon2025.BLL.Services;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.DAL.Interfaces;
using CitizenHackathon2025.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CityzenHackathon2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
    #nullable disable
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly IAIService _aiService;
        private readonly IHubContext _hubContext;

        public SuggestionController(ISuggestionRepository suggestionRepository, IHubContext hubContext)
        {
            _suggestionRepository = suggestionRepository;
            _hubContext = hubContext;
        }
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestSuggestion()
        {
            var suggestions = await _suggestionRepository.GetLatestSuggestionAsync(); // 👈 appel correct
            return Ok(suggestions);
        }
        [HttpPost]
        public async Task<IActionResult> SaveSuggestion([FromBody] Suggestion @suggestion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var savedSuggestion = await _suggestionRepository.SaveSuggestionAsync(@suggestion); // 👈 correction du paramètre

            if (savedSuggestion == null)
                return StatusCode(500, "Erreur lors de l'enregistrement");

            // ✅ Diffusion en temps réel
            await _hubContext.Clients.All.SendAsync("NewSuggestion", new
            {
                //EventId = id,
                Suggestion = savedSuggestion

            });

            return Ok(savedSuggestion);
        }
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateSuggestion([FromBody] WeatherForecast forecast, string suggestion)
        {
            var prompt = $"Il fait {forecast.TemperatureC}°C avec {forecast.Humidity}% d'humidité à {forecast.Location}.";
            //var suggestion = await _aiService.GetTouristicSuggestionsAsync(prompt);

            //await _suggestionRepository.SaveSuggestionAsync(suggestion);
            await _hubContext.Clients.All.SendAsync("NewSuggestion", suggestion);

            return Ok(new { Suggestion = suggestion });
        }
    }
}
