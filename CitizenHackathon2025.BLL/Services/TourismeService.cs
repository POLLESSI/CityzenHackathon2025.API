using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.BLL.Interfaces;

namespace CitizenHackathon2025.BLL.Services
{
    public class TourismeService : ITourismeService
    {
        private readonly IAIService _aiService;

        public TourismeService(IAIService aiService)
        {
            _aiService = aiService;
        }

        public async Task<string> GetSmartSuggestionsAsync(string userContext)
        {
            string prompt = $"Here is the context : {userContext}. Suggest me 3 original and little-known activities.";
            return await _aiService.GetTouristicSuggestionsAsync(prompt);
        }
    }
}
