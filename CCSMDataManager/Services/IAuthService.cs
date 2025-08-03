using CCSMDataManager.Entities;
using CCSMDataManager.Models;

namespace CCSMDataManager.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request);
    }
}