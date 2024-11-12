using Microsoft.AspNetCore.Identity;
using OverSightTest.Interfaces;

namespace OverSightTest.Services
{
    public class AccountService :IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<Response<IdentityResult>> AddUserAsync(string username, string password)
        {
            Response<IdentityResult> response = new();

            try
            {
                var user = new IdentityUser { UserName = username };
                var result = await _userManager.CreateAsync(user, password);
                response.Result = result;
            }
            catch (Exception)
            {
                response.SetError(Codes.GeneralError, "error in add user");
            }
            return response;
        }

        public async Task<Response<SignInResult>> LoginUserAsync(string username, string password)
        {
            Response<SignInResult> response = new();
            try
            {
                var signInResult = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
                response.Result = signInResult;
            }
            catch (Exception ex)
            {
                response.SetError(Codes.GeneralError, "error in login to user");
            }
            return response;
        }

        public async Task<Response> LogoutUserAsync()
        {
            Response response = new();
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception)
            {
                response.SetError(Codes.GeneralError, "error in logout");
            }  
            return response;
        }

        public async Task<Response> CreateRoleAsync(string roleName)
        {
            Response response = new();
            try
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            catch (Exception)
            {
                response.SetError(Codes.GeneralError, "error in create role");
            }
            return response;
        }
       
        public async Task<Response> AssignRoleToUserAsync(string username, string roleName)
        {
            Response response = new();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user != null && await _roleManager.RoleExistsAsync(roleName))
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            catch (Exception)
            {
                response.SetError(Codes.GeneralError, "error in assign role to user");
            }   
            return response;
        }
    }
}
