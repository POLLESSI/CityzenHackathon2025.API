using System.Numerics;
using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.DAL.Interfaces;
using CitizenHackathon2025.DAL.Repositories;
using CityzenHackathon2025.API.Hubs;
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
        private readonly ITrafficApiService _trafficApiService;
        private readonly IHubContext<TrafficHub> _hubContext;

        public TrafficConditionController(ITrafficConditionRepository trafficConditionRepository, ITrafficApiService trafficApiService, IHubContext<TrafficHub> hubContext)
        {
            _trafficConditionRepository = trafficConditionRepository;
            _trafficApiService = trafficApiService;
            _hubContext = hubContext;
        }

        // 1) Endpoint to retrieve the latest in the database
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestTrafficCondition()
        {
            var trafficConditions = await _trafficConditionRepository.GetLatestTrafficConditionAsync(); // 👈 correct call
            return Ok(trafficConditions);
        }
        // 2) Endpoint for live fetch from Waze
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent(double lat, double lon)
        {
            var dto = await _trafficApiService.GetCurrentTrafficAsync(lat, lon);
            if (dto == null) return NotFound();

            // Mapper DTO → Entity
            var entity = new TrafficCondition
            {
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                DateCondition = dto.DateCondition,
                CongestionLevel = dto.CongestionLevel,
                IncidentType = dto.IncidentType,
                Active = true
            };
            // Sauvegarde en base et diffusion
            var saved = await _trafficConditionRepository.SaveTrafficConditionAsync(entity);
            await _hubContext.Clients.All.SendAsync("NewTrafficCondition", saved);
            return Ok(saved);
        }
        [HttpPost]
        public async Task<IActionResult> SaveTrafficCondition([FromBody] TrafficCondition @trafficCondition)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var savedTrafficCondition = await _trafficConditionRepository.SaveTrafficConditionAsync(@trafficCondition); // 👈 parameter correction

            if (savedTrafficCondition == null)
                return StatusCode(500, "Erreur lors de l'enregistrement");

            // ✅ Real-time broadcasting
            await _hubContext.Clients.All.SendAsync("NewPlace", savedTrafficCondition);

            return Ok(savedTrafficCondition);
        }
    }
}
