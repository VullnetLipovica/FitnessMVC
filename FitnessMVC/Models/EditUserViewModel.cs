using Microsoft.AspNetCore.Identity;

namespace FitnessMVC.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserRole { get; set; } // Represent the selected role directly
        public List<IdentityRole>? AllRoles { get; set; }
    }
}
