using Microsoft.AspNetCore.Http;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.DAL.Interfaces;
using CityzenHackathon2025.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using CityzenHackathon2025.API.DTOs;

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
        [HttpGet("upcoming-outdoor")]
        public async Task<IActionResult> GetOutdoorEvents()
        {
            var events = await _eventRepository.GetUpcomingOutdoorEventsAsync();
            return Ok(events);
        }
        [HttpPost("save")]
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
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDTO dto)
        {
            var newEvent = new Event
            {
                Name = dto.Name,
                Latitude = dto.Latitude,
                DateEvent = dto.DateEvent,
                IsOutdoor = dto.IsOutdoor
            };

            var created = await _eventRepository.CreateEventAsync(newEvent);
            return CreatedAtAction(nameof(GetOutdoorEvents), new { id = created.Id }, created);
        }
    }
}

