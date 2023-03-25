using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Shared.Lib.Calculator.Models
{
    public record PortionGramsResult
    {
        public required Product Product { get; init; }

        public required double ProteinGrams { get; init; }
        public required double FatGrams { get; init; }
        public required double CarbohydrateGrams { get; init; }
    }
}
