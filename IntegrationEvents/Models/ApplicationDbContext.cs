using System.Data.Entity;
using IntegrationEvents.Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IntegrationEvents.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<TrainingCourse> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Newsfeed> Newsfeeds { get; set; }
    }
}