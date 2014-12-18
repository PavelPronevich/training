using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace WebLayer.Models
{
    
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Persons")
        {
        }
    }
}