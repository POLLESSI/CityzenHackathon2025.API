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
        private readonly IDbConnection _connection;

        public TrafficConditionRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TrafficCondition?>> GetLatestTrafficConditionAsync()
        {
            try
            {
                const string sql = @"
            SELECT TOP 10 * FROM TrafficCondition
            WHERE Active = 1
            ORDER BY DateCondition DESC";
                var list = await _connection.QueryAsync<TrafficCondition>(sql);
                return list;
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
                const string sql = @"
                INSERT INTO TrafficCondition
                (Latitude, Longitude, DateCondition, CongestionLevel, IncidentType, Active)
                VALUES
                (@Latitude, @Longitude, @DateCondition, @CongestionLevel, @IncidentType, 1);
                SELECT CAST(SCOPE_IDENTITY() AS int)";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Latitude", trafficCondition.Latitude);
                parameters.Add("@Longitude", trafficCondition.Longitude);
                parameters.Add("@DateCondition", trafficCondition.DateCondition);
                parameters.Add("@CongestionLevel", trafficCondition.CongestionLevel);
                parameters.Add("@IncidentType", trafficCondition.IncidentType);

                var newId = await _connection.ExecuteScalarAsync<int>(sql, parameters);
                trafficCondition.Id = newId; // Fix: Assign the new ID to the trafficCondition object instead of parameters
                return trafficCondition;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Traffic Condition: {ex}");
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
