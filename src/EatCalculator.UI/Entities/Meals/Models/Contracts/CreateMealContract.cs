namespace EatCalculator.UI.Entities.Meals.Models.Contracts
{
    internal sealed record CreateMealContract
    {
        public required int DayId { get; init; }

        public required string Title { get; init; }
    }
}
