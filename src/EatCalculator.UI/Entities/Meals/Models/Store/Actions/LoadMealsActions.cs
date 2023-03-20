using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Meals.Models.Store.Actions
{
    internal record LoadMealsAction : BaseAction
    {
        public required int DayId { get; init; }
    }
    internal record LoadMealsFailureAction : BaseFailureAction;
    internal record LoadMealsSuccessAction : BaseSuccessAction
    {
        public required IEnumerable<Meal> Meals { get; init; }
    }
}
