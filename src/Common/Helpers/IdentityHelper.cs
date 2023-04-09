using System.Globalization;
using System.Security.Claims;
using System.Text.Json;

namespace Common.Helpers
{
    public static class IdentityHelper
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var payload = token.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            var claims = keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.Value?.ToString() ?? ""))
                ?? Enumerable.Empty<Claim>();
            return claims.ToList();
        }

        public static DateTime GetExpireTimeFromClaims(IEnumerable<Claim> claims)
        {
            var expClaim = claims.FirstOrDefault(x => x.Type.Equals("Expires"));
            return Convert.ToDateTime(expClaim!.Value, CultureInfo.InvariantCulture);
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
