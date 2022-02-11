using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pos.users.Models;
using pos.users.Services;
using pos.web.Services;

namespace pos.web.Controllers
{
    public class AuthController : ApplicationBaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenGeneratorService _tokenService;

        public AuthController(IUserService userService, ITokenGeneratorService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var valid = await _userService.IsValidUserAccountAsync(user);
            if (valid)
            {
                var userToken = await _userService.GetUserTokenInfoAsync(user.Username);
                var token = _tokenService.GetToken(userToken, 0);

                return Ok(new
                {
                    token,
                });
            }

            return BadRequest(new
            {
                statusCode = "invalid_user_account"
            });
        }
    }
}
