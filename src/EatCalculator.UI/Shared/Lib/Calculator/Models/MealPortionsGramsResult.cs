namespace EatCalculator.UI.Shared.Lib.Calculator.Models
{
    public record MealPortionsGramsResult
    {
        public required List<PortionGramsResult> PortionGramsResults { get; init; }
    }
}
