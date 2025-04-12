using Microsoft.AspNetCore.Http;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.DAL.Interfaces;
using CityzenHackathon2025.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace CityzenHackathon2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IHubContext _hubContext;

        public EventController(IEventRepository eventRepository, IHubContext hubContext)
        {
            _eventRepository = eventRepository;
            _hubContext = hubContext;
        }
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestEvent()
        {
            var events = await _eventRepository.GetLatestEventAsync(); // 👈 appel correct
            return Ok(events);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEvent([FromBody] Event @event)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var savedEvent = await _eventRepository.SaveEventAsync(@event); // 👈 correction du paramètre

            if (savedEvent == null)
                return StatusCode(500, "Erreur lors de l'enregistrement");

            // ✅ Diffusion en temps réel
            await _hubContext.Clients.All.SendAsync("NewEvent", savedEvent);

            return Ok(savedEvent);
        }
    }
}
