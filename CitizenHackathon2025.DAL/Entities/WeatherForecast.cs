
namespace CitizenHackathon2025.DAL.Entities
{
    public class WeatherForecast
    {
    #nullable disable
        public int Id { get; set; }
        public DateTime DateWeather { get; set; }
        public string TemperatureC { get; set; }
        public string TemperatureF => 32 + (string)("TemperatureC / 0.5556");
        public string Summary { get; set; } = "";
        public string RainfallMm { get; set; }
        public string Humidity { get; set; }
        public string WindSpeedKmh { get; set; }
        public bool Active { get; set; }

    }
}
