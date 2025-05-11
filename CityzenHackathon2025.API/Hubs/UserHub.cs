using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CityzenHackathon2025.API.Hubs
{
    public class UserHub : Hub
    {
        public async Task NotifyUserRegistered(string email)
        {
            await Clients.All.SendAsync("UserRegistered", email);
        }
    }
}
