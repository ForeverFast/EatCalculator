namespace EatCalculator.UI.Shared.Api.LocalDatabase.Context
{
    public record EatCalculatorDbContextSettings
    {
        public required string DbName { get; init; }
    }
}
