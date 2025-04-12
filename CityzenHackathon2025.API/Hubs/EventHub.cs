using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CityzenHackathon2025.API.Hubs
{
    public class EventHub : Hub
    {
    #nullable disable

        private readonly ILogger<EventHub> _logger;

        public EventHub(ILogger<EventHub> logger)
        {
            _logger = logger;
        }

        public async Task RefreshEvent()
        {
            _logger.LogInformation("NotifyNewEvent called");
            await Clients.All.SendAsync("NewEvent");
        }
    }
}
