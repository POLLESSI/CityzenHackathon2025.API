using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.Shared.DTOs;
using static CitizenHackathon2025.DAL.Entities.User;


namespace CitizenHackathon2025.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllActiveUsersAsync();
        Task<bool> RegisterUserAsync(string email, byte[] passwordHash, User user); 
        Task<bool> LoginAsync(LoginDTO loginDTO);
        Task DeactivateUserAsync(int id); 
        void SetRole(int id, string? role);
    }
}
