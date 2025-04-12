using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CityzenHackathon2025.API.DTOs
{
    public class EventDTO
    {
    #nullable disable
        [DisplayName("Event Name : ")]
        public string Name { get; set; }
        [DisplayName("Latitude : ")]
        public string Latitude { get; set; }
        [DisplayName("Longitude : ")]
        public string Longitude { get; set; }
        [DisplayName("Event Date : ")]
        public DateTime DateEvent { get; set; }
        [DisplayName("Expected Crowd : ")]
        public string ExpectedCrowd { get; set; }
    }
}
