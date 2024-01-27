using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.Models
{
    public class FeedBack
    {
        [Key]
        public int id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }
        
        [StringLength(1000)]
        public string Text { get; set; }
    }
}
