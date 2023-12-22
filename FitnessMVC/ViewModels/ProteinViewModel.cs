using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.ViewModels
{
    public class ProteinViewModel
    {
        [Key]
        public int ProteinId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public IFormFile photo { get; set; }
    }
}
