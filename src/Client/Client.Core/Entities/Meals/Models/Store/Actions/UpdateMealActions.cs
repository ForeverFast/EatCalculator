using Client.Core.Entities.Meals.Models.Contracts;

namespace Client.Core.Entities.Meals.Models.Store.Actions
{
    internal record UpdateMealAction : BaseAction
    {
        public required int Id { get; init; }
        public required UpdateMealContract Meal { get; init; }
    }
    internal record UpdateMealFailureAction : BaseFailureAction;
    internal record UpdateMealSuccessAction : BaseSuccessAction
    {
        public required Meal Meal { get; init; }
    }
}
