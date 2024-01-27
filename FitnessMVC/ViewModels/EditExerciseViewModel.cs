using FitnessMVC.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.ViewModels
{
    public class EditExerciseViewModel
    {
        public int ExerciseId { get; set; }

        [Required]
        [StringLength(255)]
        public string exName { get; set; }

        [Required]
        [StringLength(255)]
        public string exDescription { get; set; }

       
        public BodyParts BodyParts { get; set; }

        public Difficulty Difficulty { get; set; }

        // Property for the new image
        public IFormFile? NewImage { get; set; }



    }
}
