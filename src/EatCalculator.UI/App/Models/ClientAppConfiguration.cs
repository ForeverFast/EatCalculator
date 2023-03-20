using EatCalculator.UI.Shared.Lib.AppBuilder;

namespace EatCalculator.UI.App.Models
{
    public sealed record ClientAppConfiguration
    {
        public ClientAppPlatform Platform { get; init; }
    }
}
