using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;

namespace CitizenHackathon2025.BLL.Interfaces
{
    public interface IUserHubService
    {
        Task NotifyUserRegistered(User user);
    }
}
