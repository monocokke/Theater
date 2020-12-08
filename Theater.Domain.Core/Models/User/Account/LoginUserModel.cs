using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.User.Account
{
    public class LoginUserModel
    {
        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
