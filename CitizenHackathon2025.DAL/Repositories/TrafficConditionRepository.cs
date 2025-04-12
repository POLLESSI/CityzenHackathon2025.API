using System;
using Dapper;
using System.Data.SqlClient;
using CitizenHackathon2025.DAL.Interfaces;
using CitizenHackathon2025.DAL.Entities;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CitizenHackathon2025.DAL.Repositories
{
    public class TrafficConditionRepository : ITrafficConditionRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;
        public async Task<IEnumerable<TrafficCondition?>> GetLatestTrafficConditionAsync()
        {
            try
            {
                string sql = " SELECT * FROM TrafficCondition Where Active = 1";

                var trafficConditions = await _connection.QueryAsync<TrafficCondition?>(sql);
                return [.. trafficConditions];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Traffic Condition: {ex.Message}");
                return [];
            }
        }

        public async Task<TrafficCondition> SaveTrafficConditionAsync(TrafficCondition trafficCondition)
        {
            try
            {
                string sql = "INSERT INTO TrafficCondition (Latitude, Longitude, DateCondition, CongestionLevel, IncidentType)" +
                "VALUES (@Latitude, @Longitude, @DateCondition, @CongestionLevel, @IncidentType)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Latitude", trafficCondition.Latitude);
                parameters.Add("@Longitude", trafficCondition.Longitude);
                parameters.Add("@DateCondition", trafficCondition.DateCondition);
                parameters.Add("@CongestionLevel", trafficCondition.CongestionLevel);
                parameters.Add("@IncidentType", trafficCondition.IncidentType);

                int rowsAffected = await _connection.ExecuteAsync(sql, parameters);
                return null;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error adding Traffic Traffic Condition: {ex.ToString()}");
                return null;
            }
        }
        public async Task<Event?> GetByIdAsync(int id)
        {
            try
            {
                const string sql = "SELECT * FROM TrafficCondition WHERE Id = @Id AND Active = 1";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int64);

                var trafficCondition = await _connection.QueryFirstOrDefaultAsync<TrafficCondition?>(sql, parameters);

                return null;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Traffic Condition : {ex.ToString}");
                return null;
            }

        }
    }
}
