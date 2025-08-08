using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace CCSMDataManager.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [MaxLength(2000)]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Role { get; set; } = "User"; // Default role is User  
    }
}
