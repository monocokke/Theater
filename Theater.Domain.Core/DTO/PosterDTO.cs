using System;
using System.Collections.Generic;
using System.Text;

namespace Theater.Domain.Core.DTO
{
    public class PosterDTO
    {
        public int Id { get; set; }
        public DateTime? DateTime { get; set; }
        public bool? Premiere { get; set; }
        public int PerformanceId { get; set; }
    }
}
