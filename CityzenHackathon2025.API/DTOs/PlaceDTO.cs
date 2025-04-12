using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CityzenHackathon2025.API.DTOs
{
    public class PlaceDTO
    {
#nullable disable
        [DisplayName("Place Name : ")]
        public string Name { get; set; }
        [DisplayName("Place Type : ")]
        public string Type { get; set; }
        [DisplayName("Indoor ? : ")]
        public string Indoor { get; set; }
        [DisplayName("Latitude : ")]
        public string Latitude { get; set; }
        [DisplayName("Longitude : ")]
        public string Longitude { get; set; }
        [DisplayName("Coordonates : ")]
        public string Coordonates { get; set; }
        [DisplayName("Capacity : ")]
        public string Capacity { get; set; }
        [DisplayName("Tags : ")]
        public string Tags { get; set; }
    }
}
