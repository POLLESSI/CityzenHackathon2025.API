using CitizenHackathon2025.DAL.Interfaces;
using Microsoft.AspNetCore.SignalR;
using CityzenHackathon2025.Shared.DTOs;
using CityzenHackathon2025.API.Hubs;
using CityzenHackathon2025.API.Tools;
//using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityzenHackathon2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrowdInfoController : ControllerBase
    {
    #nullable disable
        private readonly ICrowdInfoRepository _crowdInfoRepository;
        private readonly IHubContext<CrowdHub> _hubContext;

        public CrowdInfoController(ICrowdInfoRepository crowdInfoRepository, IHubContext<CrowdHub> hubContext)
        {
            _crowdInfoRepository = crowdInfoRepository;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCrowdInfo([FromBody] CrowdInfoDTO crowdInfoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var crowdInfo = crowdInfoDTO.MapToCrowdInfo();
            var savedCrowdInfo = await _crowdInfoRepository.SaveCrowdInfoAsync(crowdInfo);

            if (savedCrowdInfo == null)
                return StatusCode(500, "Erreur lors de l'enregistrement");

            // 👇 ici tu peux utiliser SendAsync tranquillement
            await _hubContext.Clients.All.SendAsync("NewCrowdInfo", savedCrowdInfo.MapToCrowdInfoDTO());

            return Ok(savedCrowdInfo.MapToCrowdInfoDTO());
        }
    }
}
