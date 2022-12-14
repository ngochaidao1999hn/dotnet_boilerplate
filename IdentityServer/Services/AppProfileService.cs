using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class AppProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;
        public AppProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No sub claim present");
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                throw new Exception($"No user found matching subject Id: {sub}");
            }
            var role = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
                {
                    new Claim("full_name", user.UserName),
                    new Claim("email",user.Email),
                };
            foreach (var userRole in role)
            {
                claims.Add(new Claim("role", userRole));
            }
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null);
        }
    }
}
