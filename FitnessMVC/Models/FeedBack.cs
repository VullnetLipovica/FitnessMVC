using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.Models
{
    public class FeedBack
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
