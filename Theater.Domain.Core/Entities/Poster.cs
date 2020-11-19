using System;

namespace Theater.Domain.Core.Entities
{
    public class Poster
    {
        public int Id { get; set; }
        public DateTime? DateTime { get; set; }
        public bool? Premiere { get; set; }

        public int PerformanceId { get; set; }
        public Performance Performance { get; set; }
    }
}
