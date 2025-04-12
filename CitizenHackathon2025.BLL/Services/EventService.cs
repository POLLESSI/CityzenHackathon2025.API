using Microsoft.AspNetCore.SignalR.Client;
using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.BLL;
using CitizenHackathon2025.DAL.Repositories;
using CitizenHackathon2025.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Components;
using CitizenHackathon2025.DAL.Entities;
namespace CitizenHackathon2025.BLL.Services
{
    public class EventService : IEventService
    {
    #nullable disable
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetLatestEventAsync()
        {
            var events = await _eventRepository.GetLatestEventAsync();
            return events;
        }

        public async Task<Event> SaveEventAsync(Event @event)
        {
            return await _eventRepository.SaveEventAsync(@event);
        }
    }
}
