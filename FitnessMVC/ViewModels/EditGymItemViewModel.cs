using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.ViewModels
{
	public class EditGymItemViewModel
	{
		public int GymItemId { get; set; }

        [Required]
        [StringLength(255)]
		public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        
        [Required]
        public int Price { get; set; }

        public IFormFile? NewImage { get; set; }
    }
}
