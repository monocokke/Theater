using System.Collections.Generic;

namespace Theater.Domain.Core.Entities
{
    public class Performance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Audience { get; set; }

        public List<Role> Roles { get; set; }
    }
}
