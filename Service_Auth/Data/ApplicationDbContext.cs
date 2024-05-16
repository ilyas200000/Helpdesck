using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service_Auth.Models;

namespace Service_Auth.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int> , int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(
                entity =>
                {
                    entity.Property(e => e.Roles).HasConversion(
                    v => string.Join(',', v.Select(r => r.ToString())),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(r => Enum.Parse<UserRole>(r)).ToList()
                    );

                }
                );
        }
    }
}
