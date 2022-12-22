using Application.Models;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Infrastructure.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IConfiguration _configuration;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _configuration = configuration;
        }

        public async Task<List<ApplicationUser>> GetListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<string?> GetUserNameAsync(int userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<bool> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }

        public async Task<bool> IsInRoleAsync(int userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<AuthenticationResponse> AuthorizeAsync(string userName, string password)
        {
            AuthenticationResponse res = new AuthenticationResponse();
            ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                res.accessToken = null;
            }
            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var userClaimPrincipals = await _userClaimsPrincipalFactory.CreateAsync(user);
                var claims = userClaimPrincipals.Claims;
                var issuer = _configuration["JWT:Issuer"];
                var audience = _configuration["JWT:Audience"];
                var securityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var credentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256);
                var expDate = DateTime.Now.AddDays(1);
                var token = new JwtSecurityToken(issuer: issuer,
                    audience: audience,
                    signingCredentials: credentials,
                    claims: claims,
                    expires: expDate);
                var tokenHandler = new JwtSecurityTokenHandler();
                var stringToken = tokenHandler.WriteToken(token);
                res.accessToken = stringToken;
                res.expiredDate = expDate;
            }

            return res;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            return user != null ? await DeleteUserAsync(user) : false;
        }

        public async Task<bool> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
    }
}