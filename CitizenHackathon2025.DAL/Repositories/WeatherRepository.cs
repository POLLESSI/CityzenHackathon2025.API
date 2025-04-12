using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.DAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CitizenHackathon2025.DAL.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
#nullable disable
        private readonly SqlConnection _connection;

        public WeatherRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<WeatherForecast?> GetLatestWeatherForecastAsync()
        {
            try
            {
                string sql = " SELECT * FROM WeatherForecast Where Active = 1";

                var weatherForecasts = await _connection.QueryAsync<WeatherForecast?>(sql);
                    //.OrderByDescending(w => w.Date)
                    //.FirstOrDefaultAsync();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Weather Forecast: {ex.Message}");
                return null;
            }
        }

        public async Task<WeatherForecast> SaveWeatherForecastAsync(WeatherForecast forecast)
        {
            try
            {
                const string sql = @"
                        INSERT INTO WeatherForecast (DateWeather, TemperatureC, TemperatureF, Summary, RainfallMm, Humidity, WindSpeedKmh)
                        VALUES (@DateWeather, @TemperatureC, @TemperatureF, @Summary, @RainfallMm, @Humidity, @WindSpeedKmh);
                        SELECT CAST(SCOPE_IDENTITY() AS int);";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DateWeather", forecast.DateWeather);
                parameters.Add("@TemperatureC", forecast.TemperatureC);
                parameters.Add("@TemperatureF", forecast.TemperatureF);
                parameters.Add("@Summary", forecast.Summary);
                parameters.Add("@RainfallMm", forecast.RainfallMm);
                parameters.Add("@Humidity", forecast.Humidity);
                parameters.Add("@WindSpeedKmh", forecast.WindSpeedKmh);

                // Récupération l'ID généré automatiquement
                var id = await _connection.ExecuteScalarAsync<int>(sql, parameters);
                forecast.Id = id;
                return forecast;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error adding Weather Forecast: {ex.ToString()}");
                return null;
            }
        }
        public async Task<WeatherForecast?> GetByIdAsync(int id)
        {
            try
            {
                const string sql = "SELECT * FROM WeatherForecast WHERE Id = @Id AND Active = 1";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int64);

                var weatherForecast = await _connection.QueryFirstOrDefaultAsync<WeatherForecast?>(sql, parameters);

                return weatherForecast;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Weather Forecast : {ex.ToString}");
                return null;
            }

        }
    }
}
