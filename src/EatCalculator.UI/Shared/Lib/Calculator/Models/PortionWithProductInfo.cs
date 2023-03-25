using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Shared.Lib.Calculator.Models
{
    public record PortionWithProductInfo
    {
        public required Portion Portion { get; init; }
        public required Product Product { get; init; }
    }
}
