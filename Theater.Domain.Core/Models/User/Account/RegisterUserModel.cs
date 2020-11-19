using System;
using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.User.Account
{
    public class RegisterUserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(6)]
        public string Sex { get; set; }
    }
}
