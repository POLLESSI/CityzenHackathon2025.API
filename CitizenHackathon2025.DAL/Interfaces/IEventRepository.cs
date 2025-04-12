using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using static CitizenHackathon2025.DAL.Entities.Event;


namespace CitizenHackathon2025.DAL.Interfaces
{
    public interface IEventRepository
    {
    #nullable disable
        Task<IEnumerable<Event?>> GetLatestEventAsync();
        Task<Event> SaveEventAsync(Event @event);
    }
}
