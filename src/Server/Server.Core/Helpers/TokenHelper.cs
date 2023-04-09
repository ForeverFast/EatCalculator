using Microsoft.IdentityModel.Tokens;
using Server.Core.Context.Entities.Identity;
using Server.Core.Resources;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Server.Core.Helpers
{
    internal static class TokenHelper
    {
        public static string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var signInCredentials = new SigningCredentials(TokenDefaults.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: TokenDefaults.Issuer,
                audience: TokenDefaults.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signInCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        public static string GenerateAccessToken(User user)
            => GenerateAccessToken(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
            });

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = TokenDefaults.GetSymmetricSecurityKey(),

                ValidateIssuer = false,
                ValidateAudience = false, 
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false 
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new BadRequestException("Invalid token");

            return principal;
        }
    }
}
