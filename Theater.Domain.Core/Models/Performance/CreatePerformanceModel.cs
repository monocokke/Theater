using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.Performance
{
    public class CreatePerformanceModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Genre { get; set; }

        [StringLength(20)]
        public string Audience { get; set; }
    }
}
