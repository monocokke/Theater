using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.Role
{
    public class CreateRoleModel
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required]
        [StringLength(6)]
        public string Sex { get; set; }

        public string EyeColor { get; set; }

        public string HairColor { get; set; }

        public string Nationality { get; set; }

        public int Height { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public int PerformanceId { get; set; }
    }
}
