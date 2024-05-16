using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using NuGet.Packaging;
using Service_Auth.Models;
using System.Security.Claims;

namespace Service_Auth.Service

{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            if (user == null)
                throw new ArgumentException("User not found");

            // Add user claims including roles
            var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
            new Claim(JwtClaimTypes.Name, user.UserName),
            new Claim(JwtClaimTypes.Email, user.Email),
            // Add more claims as needed
        };
            if (user.Roles != null)
            {
                // Add roles to claims
                foreach (UserRole role in user.Roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, role.ToString()));
                }
            }

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            // Check user's active status if needed
        }
    }
}
