
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CityzenHackathon2025.API.Hubs
{
    public class SuggestionHub : Hub
    {
#nullable disable

        private readonly ILogger<SuggestionHub> _logger;

        public SuggestionHub(ILogger<SuggestionHub> logger)
        {
            _logger = logger;
        }

        public async Task RefreshSuggestion()
        {
            _logger.LogInformation("NotifyNewSuggestion called");
            await Clients.All.SendAsync("NewSuggestion");
        }
    }
}
