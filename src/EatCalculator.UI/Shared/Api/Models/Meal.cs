namespace EatCalculator.UI.Shared.Api.Models
{
    internal class Meal
    {
        public required int Id { get; init; }
        public required int DayId { get; init; }

        public string? Name { get; init; }

        public int Order { get; init; }
    }
}
