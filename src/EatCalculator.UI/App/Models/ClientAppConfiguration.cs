namespace EatCalculator.UI.App.Models
{
    public sealed record ClientAppConfiguration
    {
        public required bool IsWeb { get; init; }
        public required bool IsDesktop { get; init; }
    }
}
