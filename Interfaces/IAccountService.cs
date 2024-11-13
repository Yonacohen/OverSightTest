using Microsoft.AspNetCore.Identity;

namespace OverSightTest.Interfaces
{
    public interface IAccountService
    {
        Task<Response<IdentityResult>> AddUserAsync(string username, string password);
        Task<Response<string>> LoginUserAsync(string username, string password);
        Task<Response> LogoutUserAsync();
        Task<Response> CreateRoleAsync(string roleName);
        Task<Response> AssignRoleToUserAsync(string username, string roleName);
    }
}
