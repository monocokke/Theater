using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.ActorRole
{
    public class CreateActorRoleModel
    {
        [Required]
        public int ActorId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public bool Understudy { get; set; }
    }
}
