using System.Numerics;
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
    public class TrafficConditionController : ControllerBase
    {
        private readonly ITrafficConditionRepository _trafficConditionRepository;
        private readonly IHubContext _hubContext;

        public TrafficConditionController(ITrafficConditionRepository trafficConditionRepository, IHubContext hubContext)
        {
            _trafficConditionRepository = trafficConditionRepository;
            _hubContext = hubContext;
        }
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestTrafficCondition()
        {
            var trafficConditions = await _trafficConditionRepository.GetLatestTrafficConditionAsync(); // 👈 appel correct
            return Ok(trafficConditions);
        }
        [HttpPost]
        public async Task<IActionResult> SaveTrafficCondition([FromBody] TrafficCondition @trafficCondition)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var savedTrafficCondition = await _trafficConditionRepository.SaveTrafficConditionAsync(@trafficCondition); // 👈 correction du paramètre

            if (savedTrafficCondition == null)
                return StatusCode(500, "Erreur lors de l'enregistrement");

            // ✅ Diffusion en temps réel
            await _hubContext.Clients.All.SendAsync("NewPlace", savedTrafficCondition);

            return Ok(savedTrafficCondition);
        }
    }
}
