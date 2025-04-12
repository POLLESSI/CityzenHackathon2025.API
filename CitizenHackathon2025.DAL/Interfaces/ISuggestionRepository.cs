using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using static CitizenHackathon2025.DAL.Entities.Suggestion;

namespace CitizenHackathon2025.DAL.Interfaces
{
    public interface ISuggestionRepository
    {
        Task<IEnumerable<Suggestion?>> GetLatestSuggestionAsync();
        Task<Suggestion> SaveSuggestionAsync(Suggestion @suggestion);
        //Task SaveSuggestionAsync(object id, string suggestion);
    }
}
