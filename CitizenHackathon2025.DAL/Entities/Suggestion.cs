
namespace CitizenHackathon2025.DAL.Entities
{
    public class Suggestion
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateSuggestion { get; set; }
        public string OriginalPlace { get; set; }
        public string SuggestedAlternatives { get; set; }
        public string Reason { get; set; }
        public bool Active { get; set; }
    }
}
