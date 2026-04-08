using System;

namespace eUseControl.Domain.Entities
{
    public class UserData
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public DateTime RegisteredOn { get; set; }
    }
}
