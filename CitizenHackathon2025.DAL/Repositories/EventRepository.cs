using Dapper;
using System.Data.SqlClient;
using CitizenHackathon2025.DAL.Interfaces;
using CitizenHackathon2025.DAL.Entities;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CitizenHackathon2025.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public EventRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Event>> GetLatestEventAsync()
        {
            try
            {
                string sql = " SELECT * FROM Event Where Active = 1";

                var events = await _connection.QueryAsync<Event?>(sql);
                return [.. events];
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error retrieving certifications: {ex.Message}");
                return [];
            }
            
        }

        public async Task<Event> SaveEventAsync(Event @event)
        {
            try
            {
                string sql = "INSERT INTO Event (Id, Name, Latitude, Longitude, DateEvent, ExpectedCrowd)" +
                "VALUES (@Id, @Name, @Latitude, @Longitude, @DateEvent, @ExpectedCrowd)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", @event.Name);
                parameters.Add("@Latitude", @event.Latitude);
                parameters.Add("@Longitude", @event.Longitude);
                parameters.Add("@DateEvent", @event.DateEvent);
                parameters.Add("@ExpectedCrowd", @event.ExpectedCrowd);

                int rowsAffected = await _connection.ExecuteAsync(sql, parameters);
                return @event;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error adding certification: {ex.ToString()}");
                return null;
            }
        }
        public async Task<Event?> GetByIdAsync(int id)
        {
            try
            {
                const string sql = "SELECT * FROM Event WHERE Id = @Id AND Active = 1";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int64);

                var @event = await _connection.QueryFirstOrDefaultAsync<Event?>(sql, parameters);

                return @event;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Event : {ex.ToString}");
                return null;
            }
            
        }
    }
}
