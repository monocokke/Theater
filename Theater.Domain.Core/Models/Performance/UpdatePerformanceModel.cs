using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.Performance
{
    public class UpdatePerformanceModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Genre { get; set; }

        [StringLength(20)]
        public string Audience { get; set; }
    }
}
