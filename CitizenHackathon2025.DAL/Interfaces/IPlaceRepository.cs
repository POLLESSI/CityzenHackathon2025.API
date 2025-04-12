using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using static CitizenHackathon2025.DAL.Entities.Place;

namespace CitizenHackathon2025.DAL.Interfaces
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Place?>> GetLatestPlaceAsync();
        Task<Place> SavePlaceAsync(Place @place);
    }
}
