namespace Client.Core.Entities.Meals.Models.Store
{
    internal static class MealStateSelectors
    {
        public static ISelector<MealState> SelectFeatureState { get; private set; }
            = SelectorFactory.CreateFeatureSelector<MealState>();

        public static ISelector<Meal?> SelectCurrentMeal { get; private set; }
            = SelectorFactory.CreateSelector(SelectFeatureState,
                state => state.Entities.Values.FirstOrDefault(x => x.Id == state.CurrentMealId));

        public static ISelector<List<Meal>> SelectMeals { get; private set; }
            = SelectorFactory.CreateSelector(SelectFeatureState, state => state.Entities.Values.ToList());
    }
}
