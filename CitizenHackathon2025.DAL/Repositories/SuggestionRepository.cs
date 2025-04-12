using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CitizenHackathon2025.DAL.Interfaces;
using CitizenHackathon2025.DAL.Entities;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CitizenHackathon2025.DAL.Repositories
{
    public class SuggestionRepository : ISuggestionRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;
        public async Task<IEnumerable<Suggestion?>> GetLatestSuggestionAsync()
        {
            try
            {
                string sql = " SELECT * FROM Suggestion Where Active = 1";

                var suggestions = await _connection.QueryAsync<Suggestion?>(sql);
                return [.. suggestions];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Suggestion: {ex.Message}");
                return [];
            }

        }

        public async Task<Suggestion> SaveSuggestionAsync(Suggestion suggestion)
        {
            try
            {
                const string sql = "INSERT INTO Suggestion (UserId, DateSuggestion, OriginalPlace SuggestedAlternatives, Reason)" +
                "VALUES (@UserId, @DateSuggestion, @OriginalPlace, @SuggestedAlternative, @Reason)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", suggestion.UserId);
                parameters.Add("@DateSuggestion", suggestion.DateSuggestion);
                parameters.Add("@OriginalPlace", suggestion.OriginalPlace);
                parameters.Add("@SuggestedAlternatives", suggestion.SuggestedAlternatives);
                parameters.Add("@Reason", suggestion.Reason);

                int rowsAffected = await _connection.ExecuteAsync(sql, parameters);
                return null;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error adding Suggestion: {ex.ToString()}");
                return null;
            }
        }
        public async Task<Suggestion?> GetByIdAsync(int id)
        {
            try
            {
                const string sql = "SELECT * FROM Suggestion WHERE Id = @Id AND Active = 1";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int64);

                var suggestion = await _connection.QueryFirstOrDefaultAsync<Suggestion?>(sql, parameters);

                return suggestion;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Suggestion : {ex.ToString}");
                return null;
            }

        }
    }
}
