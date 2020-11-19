using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.Role
{
    public class UpdateRoleModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public int Age { get; set; }

        [StringLength(6)]
        public string Sex { get; set; }

        public string EyeColor { get; set; }

        public string HairColor { get; set; }

        public string Nationality { get; set; }

        public int Height { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int PerformanceId { get; set; }
    }
}
