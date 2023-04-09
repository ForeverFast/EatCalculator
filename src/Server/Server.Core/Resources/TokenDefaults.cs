using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Server.Core.Resources
{
    public class TokenDefaults
    {
        private const string Key = "ChainsawManBestMangaBtw";

        public const string Issuer = "EatCalculatorServer";
        public const string Audience = "EatCalculatorClients";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
            => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
