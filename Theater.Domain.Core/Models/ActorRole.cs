using System;
using System.Collections.Generic;
using System.Text;

namespace Theater.Domain.Core.Models
{
    public class ActorRole
    {
        public int Id { get; set; }

        public int? ActorId { get; set; }
        public Actor Actor { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public bool? Understudy { get; set; }
    }
}
