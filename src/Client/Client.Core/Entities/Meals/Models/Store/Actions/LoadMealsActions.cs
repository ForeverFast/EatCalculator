namespace Client.Core.Entities.Meals.Models.Store.Actions
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
