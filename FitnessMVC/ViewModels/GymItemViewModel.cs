using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.ViewModels
{
    public class GymItemViewModel
    {
        [Key]
        public int GymItemId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public IFormFile photo { get; set; }
    }
}
