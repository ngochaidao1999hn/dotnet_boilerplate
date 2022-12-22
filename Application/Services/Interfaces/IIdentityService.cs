using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(int userId);

        Task<bool> IsInRoleAsync(int userId, string role);

        Task<AuthenticationResponse> AuthorizeAsync(string userName, string password);

        Task<bool> CreateUserAsync(string userName, string password);

        Task<bool> DeleteUserAsync(int userId);

        Task<List<ApplicationUser>> GetListUsers();

    }
}
