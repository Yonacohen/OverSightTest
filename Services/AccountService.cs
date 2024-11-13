using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OverSightTest.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                if (!result.Succeeded)
                {
                    response.SetError(Codes.GeneralError, string.Join(',', result.Errors.Select(err => err.Description)));
                    return response;
                }
                response.Result = result;
            }
            catch (Exception)
            {
                response.SetError(Codes.GeneralError, "error in add user");
            }
            return response;
        }

        public async Task<Response<string>> LoginUserAsync(string username, string password)
        {
            Response<string> response = new();

            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("511536EF-F270-4058-80CA-1C89C192F69A"); // Use the same secret key as in Startup

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                response.Result = tokenHandler.WriteToken(token);
                return response;
            }

            response.SetError(Codes.GeneralError, "error in user name or password");
            return response;
            //Response<SignInResult> response = new();
            //try
            //{
            //    var signInResult = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
            //    if(!signInResult.Succeeded)
            //    {
            //        response.SetError(Codes.GeneralError, "password or username not correct");
            //        return response;
            //    }
            //    response.Result = signInResult;
            //}
            //catch (Exception ex)
            //{
            //    response.SetError(Codes.GeneralError, "error in login to user");
            //}
            //return response;
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
