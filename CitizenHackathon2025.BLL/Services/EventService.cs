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

        public async Task<Event> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The event ID must be greater than zero.", nameof(id));
            }

            var eventEntity = await _eventRepository.GetByIdAsync(id);

            if (eventEntity == null || !eventEntity.Active)
            {
                return null; 
            }

            return eventEntity;
        }

        public async Task<IEnumerable<Event>> GetLatestEventAsync()
        {
            var events = await _eventRepository.GetLatestEventAsync();
            return events;
        }

        public async Task<IEnumerable<Event>> GetUpcomingOutdoorEventsAsync()
        {
            var events = await _eventRepository.GetLatestEventAsync();
            return events.Where(e => e.DateEvent > DateTime.Now);
        }

        public async Task<Event> SaveEventAsync(Event @event)
        {
            return await _eventRepository.SaveEventAsync(@event);
        }
        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            if (newEvent == null)
                throw new ArgumentNullException(nameof(newEvent));

            return await _eventRepository.CreateEventAsync(newEvent);
        }
    }
}
