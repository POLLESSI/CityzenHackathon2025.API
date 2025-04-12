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
    public class TrafficConditionService : ITrafficConditionService
    {
#nullable disable
        private readonly ITrafficConditionRepository _trafficConditionRepository;

        public TrafficConditionService(ITrafficConditionRepository trafficConditionRepository)
        {
            _trafficConditionRepository = trafficConditionRepository;
        }

        public async Task<IEnumerable<TrafficCondition?>> GetLatestTrafficConditionAsync()
        {
            var trafficConditions = await _trafficConditionRepository.GetLatestTrafficConditionAsync();
            return trafficConditions;
        }
        
        public async Task<TrafficCondition> SaveTrafficConditionAsync(TrafficCondition trafficCondition)
        {
            return await _trafficConditionRepository.SaveTrafficConditionAsync(trafficCondition);
        }
    }
}
