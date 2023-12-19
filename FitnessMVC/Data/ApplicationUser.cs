using Microsoft.AspNetCore.Identity;

namespace FitnessMVC.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

    }
}
