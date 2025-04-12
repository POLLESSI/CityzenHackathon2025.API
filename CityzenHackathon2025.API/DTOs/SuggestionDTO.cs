using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CityzenHackathon2025.API.DTOs
{
    public class SuggestionDTO
    {
#nullable disable
        [DisplayName("User ID : ")]
        public int UserId { get; set; }
        [DisplayName("Date of Suggestion : ")]
        public DateTime DateSuggestion { get; set; }
        [DisplayName("Original Place : ")]
        public string OriginalPlace { get; set; }
        [DisplayName("Suggested Alternatives : ")]
        public string SuggestedAlternatives { get; set; }
        [DisplayName("Reason : ")]
        public string Reason { get; set; }
    }
}
