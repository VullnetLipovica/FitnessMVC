using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessMVC.Models
{
    public class Membership
    {
        [Key]
        public int memId { get; set; }

        [Required]
        [StringLength(255)]
        public string memName { get; set; }
        
        [Required]
        public string memLength { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

    }
}
