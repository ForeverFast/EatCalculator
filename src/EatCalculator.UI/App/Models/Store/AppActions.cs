namespace EatCalculator.UI.App.Models.Store
{
    internal record InitializeAppAction
    {
        public required bool IsWeb { get; init; }
        public required bool IsDesktop { get; init; }
    }
}
