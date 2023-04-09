using Client.Core.Entities.Meals.Models.Contracts;

namespace Client.Core.Entities.Meals.Models.Store.Actions
{
    internal record CreateMealAction : BaseAction
    {
        public required CreateMealContract Meal { get; init; }
    }
    internal record CreateMealFailureAction : BaseFailureAction;
    internal record CreateMealSuccessAction : BaseSuccessAction
    {
        public required Meal Meal { get; init; }
    }

    internal record CreateEmptyMealAction : BaseAction
    {
        public required int DayId { get; init; }
    }
    internal record CreateEmptyMealFailureAction : BaseFailureAction;
    internal record CreateEmptyMealSuccessAction : BaseSuccessAction
    {
        public required Meal Meal { get; init; }
    }
}
