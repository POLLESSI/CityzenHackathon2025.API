using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.DAL.Interfaces;
using CityzenHackathon2025.API.DTOs;
using CitizenHackathon2025.API.Hubs;
using CitizenHackathon2025.BLL.Services;

namespace CityzenHackathon2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
#nullable disable
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly IAIService _aiService;
        private readonly IHubContext<GPTHub> _hubContext;

        public SuggestionController(ISuggestionRepository suggestionRepository, IHubContext<GPTHub> hubContext, IAIService aiService)
        {
            _suggestionRepository = suggestionRepository;
            _hubContext = hubContext;
            _aiService = aiService;
        }

        // ✅ GET Latest
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestSuggestion()
        {
            var suggestions = await _suggestionRepository.GetLatestSuggestionAsync();
            return Ok(suggestions);
        }

        // ✅ POST classique
        [HttpPost]
        public async Task<IActionResult> SaveSuggestion([FromBody] Suggestion suggestion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var savedSuggestion = await _suggestionRepository.SaveSuggestionAsync(suggestion);

            if (savedSuggestion == null)
                return StatusCode(500, "Erreur lors de l'enregistrement");

            await _hubContext.Clients.All.SendAsync("NewSuggestion", savedSuggestion);
            return Ok(savedSuggestion);
        }

        // ✅ POST AI generation + recording + SignalR
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateSuggestion([FromBody] WeatherForecastSuggestionDTO forecastDto)
        {
            var prompt = $"Il fait {forecastDto.TemperatureC}°C avec {forecastDto.Humidity}% d'humidité à {forecastDto.Location}. " +
                         $"Propose une activité ou un endroit alternatif agréable pour les habitants, avec un ton concis et engageant.";

            // Fix for CS0815: The issue occurs because the method `GetSuggestionsAsync` in `IAIService` is defined to return `Task` (void), 
            // but the code is trying to assign its result to a variable. The method should return a `Task<string>` instead.

            var gptResponse = await _aiService.GetSuggestionsAsync(prompt);

            var newSuggestion = new Suggestion
            {
                UserId = 1, // ou via ClaimsPrincipal si auth
                DateSuggestion = DateTime.UtcNow,
                OriginalPlace = forecastDto.Location,
                SuggestedAlternatives = gptResponse,
                Reason = "Généré par IA selon la météo"
            };

            var savedSuggestion = await _suggestionRepository.SaveSuggestionAsync(newSuggestion);

            await _hubContext.Clients.All.SendAsync("NewSuggestion", savedSuggestion);

            return Ok(new { Suggestion = savedSuggestion });
        }
    }
    public interface IAIService
    {
        Task<string> GetSuggestionsAsync(object content); // Updated return type to Task<string>
        Task<string> GetTouristicSuggestionsAsync(string prompt);
        Task<string> SummarizeTextAsync(string input);
        Task<string> TranslateToFrenchAsync(string englishText);
    }
}
