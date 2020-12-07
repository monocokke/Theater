namespace Theater.Domain.Core.DTO
{
    public class ActorRoleDTO
    {
        public int Id { get; set; }
        public int? ActorId { get; set; }
        public int? RoleId { get; set; }
        public bool? Understudy { get; set; }
    }
}
