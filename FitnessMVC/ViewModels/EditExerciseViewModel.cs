using FitnessMVC.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.ViewModels
{
    public class EditExerciseViewModel
    {
        public int ExerciseId { get; set; }

        [Required(ErrorMessage = "Exercise Name is required.")]
        public string exName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string exDescription { get; set; }

        [Required(ErrorMessage = "Body Part is required.")]
        public BodyParts BodyParts { get; set; }

        [Required(ErrorMessage = "Difficulty is required.")]
        public Difficulty Difficulty { get; set; }

        // Property for the new image
        public IFormFile NewImage { get; set; }



    }
}
