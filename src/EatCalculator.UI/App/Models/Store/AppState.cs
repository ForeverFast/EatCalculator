namespace EatCalculator.UI.App.Models.Store
{
    internal record AppState
    {
        public bool IsWeb { get; init; }
        public bool IsDesktop { get; init; }
    }
}
