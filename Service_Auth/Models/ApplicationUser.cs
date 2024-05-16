using Microsoft.AspNetCore.Identity;

namespace Service_Auth.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        // Property to hold multiple roles
        public ICollection<UserRole>? Roles  { get; set; }

    }
}
