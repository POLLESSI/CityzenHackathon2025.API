using Microsoft.AspNetCore.SignalR;

namespace CityzenHackathon2025.API.Hubs
{
    public class UpdateHub : Hub
    {
        public async Task SendUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }
    }
    
}
