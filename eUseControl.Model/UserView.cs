using System;

namespace eUseControl.Model
{
    public class UserView
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RegisteredOn { get; set; }
    }
}
