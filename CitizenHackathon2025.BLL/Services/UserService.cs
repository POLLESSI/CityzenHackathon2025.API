using System;
using System.Text;
using Microsoft.AspNetCore.SignalR.Client;
using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.BLL;
using CitizenHackathon2025.DAL.Repositories;
using CitizenHackathon2025.DAL.Interfaces;
using CitizenHackathon2025.Shared.DTOs;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Components;
using CitizenHackathon2025.DAL.Entities;
using Microsoft.Extensions.Logging;
using System.Drawing;
using Microsoft.AspNetCore.SignalR;

namespace CitizenHackathon2025.BLL.Services
{
    public class UserService : IUserService
    {
    #nullable disable
        private readonly IUserRepository _userRepository;
        private readonly IUserHubService _hubService;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IUserHubService hubService, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _hubService = hubService;
            _logger = logger;
        }

        public async Task DeactivateUserAsync(int id)
        {
            try
            {
                await _userRepository.DeactivateUserAsync(id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting user : {ex.ToString}");
            }
        }

        public async Task<IEnumerable<User>> GetAllActiveUsersAsync()
        {
            return await _userRepository.GetAllActiveUsersAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _userRepository.GetUserByEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user by email.");
                return null;
            }
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                return _userRepository.GetUserByIdAsync(id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting user : {ex.ToString}");
            }
            return null;
        }

        

        public async Task<bool> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                return await _userRepository.LoginAsync(loginDTO) ;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error loging : {ex.ToString}");
            }
            return false;
        }

        public async Task<bool> RegisterUserAsync(string email, byte[] passwordHash, string role)
        {
            try
            {
                var user = new User
                {
                    Email = email,
                    PasswordHash = passwordHash,
                    Role = role,
                    Active = true
                };

                //await _userRepository.RegisterUserAsync(email, passwordHash, user);
                await _hubService.NotifyUserRegistered(user);

                // Assuming the ID of the newly registered user is required as the return value.
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering new user : {ex}");
                return false;
            }
        }

        public void SetRole(int id, string? role)
        {
            try
            {
                _userRepository.SetRole(id, role);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error changing rôle: {ex.ToString}");
            }
        }
    }
}
