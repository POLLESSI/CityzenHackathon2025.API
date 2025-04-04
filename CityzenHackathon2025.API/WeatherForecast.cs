namespace CityzenHackathon2025.API
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
        public double? RainfallMm { get; set; }
        public int? Humidity { get; set; }
        public double WindSpeedKmh { get; set; }
    }
}
