
namespace CitizenHackathon2025.DAL.Entities
{
    public class Place
    {
    #nullable disable
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Indoor { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Coordonates { get; set; }
        public string Capacity { get; set; }
        public string Tags { get; set; }
        public bool Active { get; set; }
    }
}
