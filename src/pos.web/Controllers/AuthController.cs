using Microsoft.AspNetCore.Mvc;
using pos.users.Models;
using pos.users.Services;
using pos.web.Services;

namespace pos.web.Controllers
{
    public class AuthController : ApplicationBaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [Route("login")]
        //[HttpPost]
        public async Task<IActionResult> Login(UserLogin user)
        {
            var valid = await _userService.IsValidUserAccountAsync(user);
            if (valid)
            {
                var userToken = new UserToken(); // todo: load user
                // generate token and return
                var token = _tokenService.GetToken(userToken, 0); // todo: get expiry minutes

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
