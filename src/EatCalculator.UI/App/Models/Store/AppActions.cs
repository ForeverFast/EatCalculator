using EatCalculator.UI.Shared.Lib.AppBuilder;

namespace EatCalculator.UI.App.Models.Store
{
    internal record InitializeAppAction
    {
        public required ClientAppPlatform Platform { get; init; }
    }
}
