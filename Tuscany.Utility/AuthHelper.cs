using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Tuscany.Utility
{
    public class AuthHelper
    {
        public static TokenObj GetToken(List<Claim> claims, string username)
        {
            //Guid id = Guid.NewGuid();

            DateTime expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(2));
            // Creating JWT token
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new TokenObj(new JwtSecurityTokenHandler().WriteToken(jwt), username, expires);
        }
    }
}
