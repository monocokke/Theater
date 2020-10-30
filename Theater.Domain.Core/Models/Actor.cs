using System.Collections.Generic;

namespace Theater.Domain.Core.Models
{
    public class Actor
    {
        public int Id { get; set; }       

        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string Nationality { get; set; }
        public int Height { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public List<ActorRole> ActorRoles { get; set; }
    }
}
