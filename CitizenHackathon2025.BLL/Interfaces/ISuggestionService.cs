using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using static CitizenHackathon2025.DAL.Entities.Suggestion;

namespace CitizenHackathon2025.BLL.Interfaces
{
    public interface ISuggestionService
    {
#nullable disable
        Task<IEnumerable<Suggestion?>> GetLatestSuggestionAsync();
        Task<Suggestion> SaveSuggestionAsync(Suggestion @suggestion);
    }
}
