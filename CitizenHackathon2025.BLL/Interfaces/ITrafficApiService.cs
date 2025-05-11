
using CityzenHackathon2025.Shared.DTOs;

namespace CitizenHackathon2025.BLL.Interfaces
{
    public interface ITrafficApiService
    {
        /// <summary>
        /// Retrieves traffic conditions from the Waze API (Connected Citizens).
        /// </summary>
        Task<TrafficConditionDTO?> GetCurrentTrafficAsync(double latitude, double longitude);
    }
}
