using System;

namespace Theater.Domain.Core.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
    }
}
