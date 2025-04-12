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
    public class SuggestionService : ISuggestionService
    {
        private readonly ISuggestionRepository _suggestionRepository;

        public SuggestionService(ISuggestionRepository suggestionRepository)
        {
            _suggestionRepository = suggestionRepository;
        }

        public async Task<IEnumerable<Suggestion?>> GetLatestSuggestionAsync()
        {
            var suggestions = await _suggestionRepository.GetLatestSuggestionAsync();
            return suggestions;
        }

        public async Task<Suggestion> SaveSuggestionAsync(Suggestion suggestion)
        {
            return await _suggestionRepository.SaveSuggestionAsync(suggestion);
        }
    }
}
