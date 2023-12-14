using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using FitnessMVC.Data.Enum;

namespace FitnessMVC.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }


        public string exName { get; set; }

        public string exDescription { get; set; }

        public BodyParts BodyParts { get; set; }

        public Difficulty Difficulty { get; set; }

       /* [DisplayName("Image Name")]
        [AllowNull]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        [AllowNull]
        public IFormFile ImageFile { get; set; }
       */
    }
}
