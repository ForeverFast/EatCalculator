using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Meals.Models.Store.Actions
{
    internal record DeleteMealAction : BaseAction
    {
        public required int Id { get; init; }
    }
    internal record DeleteMealFailureAction : BaseFailureAction;
    internal record DeleteMealSuccessAction : BaseSuccessAction
    {
        public required int Id { get; init; }
    }

    internal record DeleteMealsAction : BaseAction
    {
        public required List<int> Ids { get; init; }
    }
    internal record DeleteMealsFailureAction : BaseFailureAction;
    internal record DeleteMealsSuccessAction : BaseSuccessAction
    {
        public required List<int> Id { get; init; }
    }
}
