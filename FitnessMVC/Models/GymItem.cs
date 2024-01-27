using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FitnessMVC.Models
{
    public class GymItem
    {
        [Key]
        public int GymItemId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        public string Image { get; set; }

    }
}
