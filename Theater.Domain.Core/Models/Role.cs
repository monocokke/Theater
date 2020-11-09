using System.Collections.Generic;

namespace Theater.Domain.Core.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Sex { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string Nationality { get; set; }
        public int? Height { get; set; }

        public int? PerformanceId { get; set; }
        public Performance Performance { get; set; }

        public List<ActorRole> ActorRoles { get; set; }
    }
}
