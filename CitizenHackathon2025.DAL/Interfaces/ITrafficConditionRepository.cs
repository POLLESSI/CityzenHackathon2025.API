using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using static CitizenHackathon2025.DAL.Entities.TrafficCondition;

namespace CitizenHackathon2025.DAL.Interfaces
{
    public interface ITrafficConditionRepository
    {
        Task<IEnumerable<TrafficCondition?>> GetLatestTrafficConditionAsync();
        Task<TrafficCondition> SaveTrafficConditionAsync(TrafficCondition @trafficCondition);
    }
}
