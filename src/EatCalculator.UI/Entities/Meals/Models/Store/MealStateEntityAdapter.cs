using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Entities.Meals.Models.Store
{
    internal sealed class MealStateEntityAdapter : EntityAdapter<int, Meal>
    {
        protected override Func<Meal, int> SelectId
            => meal => meal.Id;

        public override EntityState<int, Meal> GetInitialState()
            => new MealState { };
    }
}
