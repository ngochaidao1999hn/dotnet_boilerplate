using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(int userId);

        Task<bool> IsInRoleAsync(int userId, string role);

        Task<AuthenticationResponse> AuthorizeAsync(string userName, string password);

        Task<IdentityResult> CreateUserAsync(string userName, string password);

        Task<bool> DeleteUserAsync(int userId);

        Task<List<ApplicationUser>> GetListUsers();
    }
}