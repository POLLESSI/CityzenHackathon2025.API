using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CitizenHackathon2025.DAL.Entities;
//using static CitizenHackathon2025.DAL.Entities.TrafficCondition;

namespace CitizenHackathon2025.BLL.Services
{
    public interface IAIService
    {
        Task GetSuggestionsAsync(object content);

        /// <summary>
        /// Envoie un prompt structuré à l'IA (OpenAI) et retourne la réponse générée.
        /// </summary>
        /// <param name="prompt">Le prompt structuré contenant les contraintes touristiques.</param>
        /// <returns>Réponse textuelle générée par l'IA.</returns>
        Task<string> GetTouristicSuggestionsAsync(string prompt);
        Task<string> SummarizeTextAsync(string input);
        Task<string> TranslateToFrenchAsync(string englishText);

    }
}
