using System;
using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.Poster
{
    public class UpdatePosterModel
    {
        [Required]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool Premiere { get; set; }
        public int PerformanceId { get; set; }
    }
}
