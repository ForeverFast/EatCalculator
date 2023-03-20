using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Entities.Meals.Models.Store
{
    internal sealed record MealState : EntityState<int, Meal>
    {
        public int? CurrentMealId { get; init; }  
    }
}
