namespace EatCalculator.UI.Shared.Api.Models
{
    public record Portion
    {
        public required int Id { get; init; }
        public required int MealId { get; init; }
        public required int ProductId { get; init; }

        public required double Grams { get; init; } 
    }
}
