using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.ActorRole
{
    public class UpdateActorRoleModel
    {
        [Required]
        public int Id { get; set; }
        public int ActorId { get; set; }
        public int RoleId { get; set; }
        public bool Understudy { get; set; }
    }
}
