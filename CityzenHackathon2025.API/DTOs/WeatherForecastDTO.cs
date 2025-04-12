using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CityzenHackathon2025.API.DTOs
{
    public class WeatherForecastDTO
    {
#nullable disable
        [DisplayName("Weather Date : ")]
        public DateTime DateWeather { get; set; }
        [DisplayName("Temperature C : ")]
        public int TemperatureC { get; set; }
        [DisplayName("Temperature F : ")]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        [DisplayName("Summary : ")]
        public string Summary { get; set; } = "";
        [DisplayName("Rainfall mm : ")]
        public double RainfallMm { get; set; }
        [DisplayName("Humidity : ")]
        public int Humidity { get; set; }
        [DisplayName("Wind Speed km/h : ")]
        public double WindSpeedKmh { get; set; }
    }
}
