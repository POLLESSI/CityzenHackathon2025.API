using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using static CitizenHackathon2025.DAL.Entities.Event;


namespace CitizenHackathon2025.BLL.Interfaces
{
    public interface IEventService
    {
#nullable disable
        Task<IEnumerable<Event?>> GetLatestEventAsync();
        Task<Event> SaveEventAsync(Event @event);
        Task<IEnumerable<Event>> GetUpcomingOutdoorEventsAsync();
        Task<Event> CreateEventAsync(Event newEvent);
        Task<Event?> GetByIdAsync(int id);
    }
}
