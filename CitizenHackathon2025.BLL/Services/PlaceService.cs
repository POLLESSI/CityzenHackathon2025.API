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
using Microsoft.Extensions.Logging;

namespace CitizenHackathon2025.BLL.Services
{
    public class PlaceService : IPlaceService
    {
    #nullable disable
        private readonly IPlaceRepository _placeRepository;

        public PlaceService(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<IEnumerable<Place?>> GetLatestPlaceAsync()
        {
            var places = await _placeRepository.GetLatestPlaceAsync();
            return places;
        }

        public async Task<Place> SavePlaceAsync(Place place)
        {
            return await _placeRepository.SavePlaceAsync(place);
        }
    }
}
