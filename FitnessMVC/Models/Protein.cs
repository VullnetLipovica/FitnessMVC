using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.Models
{
    public class Protein
    {
        [Key]
        public int ProteinId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Image { get; set; }


    }
}
