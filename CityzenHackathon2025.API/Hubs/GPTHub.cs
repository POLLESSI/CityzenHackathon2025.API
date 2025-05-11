using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.SignalR;
using Hub = Microsoft.AspNetCore.SignalR.Hub;

namespace CitizenHackathon2025.API.Hubs
{
    [Authorize]
    public class GPTHub : Hub
    {
    #nullable disable
        private readonly ILogger<GPTHub> _logger;
        public GPTHub(ILogger<GPTHub> logger)
        {
            _logger = logger;
        }
        public async Task RefreshGPT()
        {
            _logger.LogInformation("RefreshGPT called");
            await Clients.All.SendAsync("notifynewGPT");
        }
        //public override async Task OnConnectedAsync()
        //{
        //    _logger.LogInformation("Connected customer : {ConnectionId}", Context.ConnectionId);
        //    await base.OnConnectedAsync();
        //}

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    _logger.LogInformation("Disconnected customer : {ConnectionId}", Context.ConnectionId);
        //    await base.OnDisconnectedAsync(exception);
        //}
    }
}
