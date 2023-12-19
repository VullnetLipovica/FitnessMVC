using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FitnessMVC.Models;

namespace FitnessMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<GymItem> GymItems { get; set; }

        public DbSet<Membership> Memberships { get; set; }
    }
}
