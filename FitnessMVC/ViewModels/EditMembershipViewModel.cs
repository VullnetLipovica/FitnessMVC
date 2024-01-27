using System.ComponentModel.DataAnnotations;

namespace FitnessMVC.ViewModels
{
    public class EditMembershipViewModel
    {
        public int memId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string memName { get; set; }

        public string memLength { get; set; }

       
    }
}
