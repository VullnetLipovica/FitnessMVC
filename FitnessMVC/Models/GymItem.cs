using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FitnessMVC.Models
{
    public class GymItem
    {
        [Key]
        public int GymItemId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        //public int? UserId { get; set; }

        //public AppUser? AppUser { get; set; }
    }
}
