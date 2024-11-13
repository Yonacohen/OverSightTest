using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OverSightTest.Interfaces;

namespace OverSightTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(Roles ="Admin") ]
        [HttpPost("user")]
        public async Task<IActionResult> AddUserAsync([FromQuery] string user, [FromQuery] string password)
        {
            var response =await _accountService.AddUserAsync(user, password);
            return response.ToActionResult();
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromQuery] string user, [FromQuery] string password)
        {
            var response = await _accountService.LoginUserAsync(user, password);
            return response.ToActionResult();
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUserAsync()
        {
            var response = await _accountService.LogoutUserAsync();
            return response.ToActionResult();
        }

    }

}
