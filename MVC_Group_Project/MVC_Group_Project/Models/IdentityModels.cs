using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MVC_Group_Project.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Address { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string RoleName { get; set; }
        
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and roles. Also initializes sample data.
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<MVC_Group_Project.Models.AccessLevel> AccessLevels { get; set; }

        public System.Data.Entity.DbSet<MVC_Group_Project.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<MVC_Group_Project.Models.SubCategory> SubCategories { get; set; }

        public System.Data.Entity.DbSet<MVC_Group_Project.Models.CreditCard> CreditCards { get; set; }

        public System.Data.Entity.DbSet<MVC_Group_Project.Models.BiddingItem> BiddingItems { get; set; }

        public System.Data.Entity.DbSet<MVC_Group_Project.Models.OnGoingBids> OnGoingBids { get; set; }


    }


}