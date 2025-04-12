using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CityzenHackathon2025.API.Hubs
{
    public class PlaceHub : Hub
    {
#nullable disable

        private readonly ILogger<PlaceHub> _logger;

        public PlaceHub(ILogger<PlaceHub> logger)
        {
            _logger = logger;
        }

        public async Task RefreshPlace()
        {
            _logger.LogInformation("NotifyNewPlace called");
            await Clients.All.SendAsync("Newplace");
        }
    }
}
