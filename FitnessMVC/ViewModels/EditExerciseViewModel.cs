using FitnessMVC.Data.Enum;

namespace FitnessMVC.ViewModels
{
    public class EditExerciseViewModel
    {
        public int ExerciseId { get; set; }


        public string exName { get; set; }

        public string exDescription { get; set; }
        public string? URL { get; set; }

        public BodyParts BodyParts { get; set; }

        public Difficulty Difficulty { get; set; }
    }
}
