namespace Client.Core.Entities.Meals.Models.Store
{
    internal sealed record MealState : EntityState<int, Meal>
    {
        public int? CurrentMealId { get; init; }
    }
}
