using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.Actor
{
    public class CreateActorModel
    {
        [Required]
        [StringLength(10)]
        public string EyeColor { get; set; }

        [Required]
        [StringLength(10)]
        public string HairColor { get; set; }

        [Required]
        [StringLength(10)]
        public string Nationality { get; set; }

        [Required]
        [Range(120, 220)]
        public int Height { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
