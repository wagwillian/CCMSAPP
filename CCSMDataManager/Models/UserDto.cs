﻿namespace CCSMDataManager.Models
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;        
        public string Role { get; set; } = "User"; // Default role is User 

    }
}
