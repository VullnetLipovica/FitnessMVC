using FitnessMVC.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.ViewModels
{
    public class ExerciseViewModel
    {
        [Key]
        public int ExerciseId { get; set; }


        public string exName { get; set; }

        public string exDescription { get; set; }

        public BodyParts BodyParts { get; set; }

        public Difficulty Difficulty { get; set; }

        public IFormFile photo { get; set; }
    }
}
