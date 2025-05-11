using Microsoft.AspNetCore.SignalR;
using CitizenHackathon2025.API.Hubs;
using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.DAL.Entities;
using CityzenHackathon2025.API.Hubs;

namespace CityzenHackathon2025.API
{
    public class UserHubService : IUserHubService
    {
        private readonly IHubContext<UserHub> _hubContext;
        public UserHubService(IHubContext<UserHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task NotifyUserRegistered(User user)
        {
            await _hubContext.Clients.All.SendAsync("UserRegistered", user);
        }
    }
}
