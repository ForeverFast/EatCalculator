namespace Client.Core.Entities.Meals.Models.Store
{
    internal sealed class MealStateEntityAdapter : EntityAdapter<int, Meal>
    {
        protected override Func<Meal, int> SelectId
            => meal => meal.Id;

        public override EntityState<int, Meal> GetInitialState()
            => new MealState { };
    }
}
