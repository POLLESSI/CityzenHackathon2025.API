using CitizenHackathon2025.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityzenHackathon2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GptController : ControllerBase
    {
#nullable disable
        private readonly IAIService _gptService;

        //[HttpPost("ask")]
        //public async Task<IActionResult> AskGpt([FromBody] GptPrompt prompt)
        //{
        //    // Fix: Ensure the method GetSuggestionsAsync returns a Task<object> instead of void
        //    var response = await _gptService.GetSuggestionsAsync(prompt.Content);
        //    return Ok(new { result = response });
        //}
    }
}
