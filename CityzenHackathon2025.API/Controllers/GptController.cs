using CitizenHackathon2025.API.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.DAL.Interfaces;
using CityzenHackathon2025.API.DTOs;
using CityzenHackathon2025.API.Hubs;

namespace CityzenHackathon2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GptController : ControllerBase
    {
        private readonly GPTRepository _gptRepository;
        private readonly IHubContext<CitizenHackathon2025.API.Hubs.GPTHub> _hubContext;

        public GptController(GPTRepository gptRepository, IHubContext<CitizenHackathon2025.API.Hubs.GPTHub> hubContext)
        {
            _gptRepository = gptRepository;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Sends a query to the AI ​​and returns an intelligent response.
        /// The response is also broadcast via SignalR to all connected clients.
        /// </summary>
        /// <param name="prompt">The text sent by the user.</param>
        /// <returns>An AI-generated response (simulated here).</returns>
        [HttpPost("ask")]
        public async Task<IActionResult> AskGpt([FromBody] GptPrompt prompt)
        {
            if (string.IsNullOrWhiteSpace((string?)(prompt?.Content)))
                return BadRequest("Prompt cannot be empty");

            // 🔁 Mock response — to be replaced later
            string generatedResponse = $"[Simulated GPT] Response to: \"{prompt.Content}\"";

            // 💾 Recording in the real GptInteractions table
            var interaction = new GPTInteraction
            {
                Prompt = (string)prompt.Content,
                Response = generatedResponse,
                CreatedAt = DateTime.UtcNow,
                Active = true
            };

            await _gptRepository.SaveInteractionAsync(interaction);

            // 📡 Sending via SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveGptResponse", new
            {
                prompt = interaction.Prompt,
                response = interaction.Response,
                createdAt = interaction.CreatedAt
            });

            return Ok(new
            {
                prompt = interaction.Prompt,
                response = interaction.Response
            });
        }

    }
}

