namespace EatCalculator.UI.Entities.Days.Models.Contracts
{
    internal sealed record UpdateDayContract
    {
        public required string Title { get; init; }
        public required int MealCount { get; init; }
    }
}
