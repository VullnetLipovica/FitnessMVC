using FitnessMVC.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.ViewModels
{
    public class ExerciseViewModel
    {
        [Key]
        public int ExerciseId { get; set; }


        [Required]
        [StringLength(255)]
        public string exName { get; set; }
       
        [StringLength(1000)]
        public string exDescription { get; set; }
        
        [Required]
        public BodyParts BodyParts { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        public IFormFile photo { get; set; }
    }
}
