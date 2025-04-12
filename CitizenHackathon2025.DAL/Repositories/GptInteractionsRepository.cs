using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CitizenHackathon2025.DAL.Repositories
{
    public class GptInteractionsRepository
    {
#nullable disable
        private readonly SqlConnection _connection;
        public async Task SaveInteractionAsync(string prompt, string gptResponse)
        {
            string sql = "INSERT INTO GptInteractions (Id, Prompt, Response) VALUES (@Id, @Prompt, @Response)";
            await _connection.ExecuteAsync(sql, new
            {
                Id = Guid.NewGuid(),
                Prompt = prompt,
                Response = gptResponse
            });
        }
    }   
}
