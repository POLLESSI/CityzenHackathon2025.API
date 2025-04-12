using CitizenHackathon2025.BLL.Services;
using CityzenHackathon2025.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CityzenHackathon2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourismController : ControllerBase
    {
#nullable disable
        private readonly IAIService _aiService;

        public TourismController(IAIService aiService)
        {
            _aiService = aiService;
        }
        [HttpPost("suggest")]
        public async Task<IActionResult> GetSuggestions([FromBody] TouristicPromptDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Prompt))
                return BadRequest("Le prompt ne peut pas être vide.");

            var response = await _aiService.GetTouristicSuggestionsAsync(dto.Prompt);

            return Ok(new { suggestions = response });
        }
    }
}
