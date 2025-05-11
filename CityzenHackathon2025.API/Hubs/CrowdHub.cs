using Microsoft.AspNetCore.SignalR;

namespace CityzenHackathon2025.API.Hubs
{
    public class CrowdHub : Hub
    {
#nullable disable

        private readonly ILogger<CrowdHub> _logger;

        public CrowdHub(ILogger<CrowdHub> logger)
        {
            _logger = logger;
        }

        public async Task RefreshCrowd()
        {
            _logger.LogInformation("RefreshCrowd called");
            await Clients.All.SendAsync("notifynewCrowd");
        }
    }
}
