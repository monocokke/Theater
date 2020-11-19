using System;
using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.Poster
{
    public class CreatePosterModel
    {
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public bool Premiere { get; set; }

        [Required]
        public int PerformanceId { get; set; }
    }
}
