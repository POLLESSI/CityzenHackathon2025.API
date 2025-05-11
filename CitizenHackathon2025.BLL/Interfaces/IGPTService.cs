using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;

namespace CitizenHackathon2025.BLL.Interfaces
{
    public interface IGPTService
    {
        Task<IEnumerable<Suggestion>> GetAllSuggestionsAsync();
        Task<IEnumerable<Suggestion>> GetSuggestionsByEventIdAsync(int id);
        Task<IEnumerable<Suggestion>> GetSuggestionsByForecastIdAsync(int id);
        Task<IEnumerable<Suggestion>> GetSuggestionsByTrafficIdAsync(int id);
        Task SaveSuggestionAsync(Suggestion suggestion);
        Task DeleteSuggestionAsync(int id);
    }
}
