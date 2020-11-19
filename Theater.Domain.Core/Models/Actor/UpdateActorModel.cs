using System.ComponentModel.DataAnnotations;

namespace Theater.Domain.Core.Models.Actor
{
    public class UpdateActorModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(10)]
        public string EyeColor { get; set; }

        [StringLength(10)]
        public string HairColor { get; set; }

        [StringLength(10)]
        public string Nationality { get; set; }

        [Range(120, 220)]
        public int? Height { get; set; }
    }
}
