using EatCalculator.UI.Entities.Meals.Models.Store.Actions;

namespace EatCalculator.UI.Entities.Meals.Models.Store
{
    internal static class MealStateReducers
    {
        private static MealStateEntityAdapter s_adapter => (MealStateEntityAdapter)MealState.GetAdapter();



        [ReducerMethod]
        public static MealState ReduceLoadMealsAction(MealState state, LoadMealsSuccessAction _)
            => s_adapter.RemoveAll<MealState>(state);

        [ReducerMethod]
        public static MealState ReduceLoadMealsSuccessAction(MealState state, LoadMealsSuccessAction action)
            => s_adapter.SetAll<MealState>(action.Meals, state);



        [ReducerMethod]
        public static MealState ReduceCreateMealSuccessAction(MealState state, CreateMealSuccessAction action)
            => s_adapter.Add<MealState>(action.Meal, state);

        [ReducerMethod]
        public static MealState ReduceCreateEmptyMealSuccessAction(MealState state, CreateEmptyMealSuccessAction action)
            => s_adapter.Add<MealState>(action.Meal, state);

        [ReducerMethod]
        public static MealState ReduceUpdateMealSuccessAction(MealState state, UpdateMealSuccessAction action)
            => s_adapter.Update<MealState>(action.Meal, state);

        [ReducerMethod]
        public static MealState ReduceDeleteMealSuccessAction(MealState state, DeleteMealSuccessAction action)
            => s_adapter.Remove<MealState>(action.Id, state);
    }
}
