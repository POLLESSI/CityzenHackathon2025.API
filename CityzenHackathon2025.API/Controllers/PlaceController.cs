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
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IHubContext _hubContext;

        public PlaceController(IPlaceRepository placeRepository, IHubContext hubContext)
        {
            _placeRepository = placeRepository;
            _hubContext = hubContext;
        }
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestPlace()
        {
            var places = await _placeRepository.GetLatestPlaceAsync(); // 👈 appel correct
            return Ok(places);
        }
        [HttpPost]
        public async Task<IActionResult> SavePlace([FromBody] Place @place)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var savedPlace = await _placeRepository.SavePlaceAsync(@place); // 👈 correction du paramètre

            if (savedPlace == null)
                return StatusCode(500, "Erreur lors de l'enregistrement");

            // ✅ Diffusion en temps réel
            await _hubContext.Clients.All.SendAsync("NewPlace", savedPlace);

            return Ok(savedPlace);
        }
    }
}
