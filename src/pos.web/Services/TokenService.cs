using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Light.GuardClauses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using pos.users.Models;

namespace pos.web.Services
{
    public interface ITokenService
    {
        string GetToken(UserToken user, int expiryMinites = 0);
    }

    public class TokenService : ITokenService
    {
        private readonly TokenSetting _tokenSetting;

        public TokenService(IOptions<TokenSetting> tokenOptions)
        {
            _tokenSetting = tokenOptions.Value;

            _tokenSetting.SecurityKey.MustNotBeNullOrEmpty();
            _tokenSetting.Issuer.MustNotBeNullOrEmpty();
            _tokenSetting.Audience.MustNotBeNullOrEmpty();
        }

        public string GetToken(UserToken user, int expiryMinites = 0)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, "user"), // todo: set user role
            };

            if (expiryMinites == 0)
            {
                expiryMinites = _tokenSetting.ExpiryMinutes;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSetting.SecurityKey));

            var token = new JwtSecurityToken(
                issuer: _tokenSetting.Issuer,
                audience: _tokenSetting.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(expiryMinites),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
