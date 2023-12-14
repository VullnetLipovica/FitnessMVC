using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessMVC.Models
{
    public class Membership
    {
        [Key]
        public int memId { get; set; }

        public string memName { get; set; }

        public string memLength { get; set; }

       /* [ForeignKey("UserId")]
        public int? UserId { get; set; }
        public AppUser? AppUser { get; set; }
       */

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

    }
}
