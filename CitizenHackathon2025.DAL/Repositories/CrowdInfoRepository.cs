using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.DAL.Interfaces;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CitizenHackathon2025.DAL.Repositories
{
    public class CrowdInfoRepository : ICrowdInfoRepository
    {
        private readonly IDbConnection _dbConnection;
        public CrowdInfoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> DeleteCrowdInfoAsync(int id)
        {
            var sql = "DELETE FROM CrowdInfos WHERE Id = @Id";
            var affectedRows = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<CrowdInfo>> GetAllCrowdInfoAsync()
        {
            var sql = "SELECT * FROM CrowdInfos";
            var result = await _dbConnection.QueryAsync<CrowdInfo>(sql);
            return result;
        }

        public async Task<CrowdInfo?> GetCrowdInfoByIdAsync(int id)
        {
            var sql = "SELECT * FROM CrowdInfos WHERE Id = @Id";
            var result = await _dbConnection.QuerySingleOrDefaultAsync<CrowdInfo>(sql, new { Id = id });
            return result;
        }

        public async Task<CrowdInfo?> SaveCrowdInfoAsync(CrowdInfo crowdInfo)
        {
            var sql = @"
                INSERT INTO CrowdInfos (LocationName, Latitude, Longitude, CrowdLevel, Timestamp)
                VALUES (@LocationName, @Latitude, @Longitude, @CrowdLevel, @Timestamp);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            var id = await _dbConnection.QuerySingleAsync<int>(sql, crowdInfo);
            crowdInfo.Id = id;
            return crowdInfo;
        }
        // Implement other methods as needed
    }
    
}
