using Client.Core.Entities.Meals.Models.Store.Actions;

namespace Client.Core.Entities.Meals.Models.Store
{
    internal static class MealStateReducers
    {
        private static readonly EntityAdapter<int, Meal> s_adapter = new(meal => meal.Id);

        [ReducerMethod]
        public static MealState ReduceLoadMealsAction(MealState state, LoadMealsSuccessAction _)
            => s_adapter.RemoveAll(state);

        [ReducerMethod]
        public static MealState ReduceLoadMealsSuccessAction(MealState state, LoadMealsSuccessAction action)
            => s_adapter.SetAll(state, action.Meals);

        [ReducerMethod]
        public static MealState ReduceCreateMealSuccessAction(MealState state, CreateMealSuccessAction action)
            => s_adapter.Add(state, action.Meal);

        [ReducerMethod]
        public static MealState ReduceCreateEmptyMealSuccessAction(MealState state, CreateEmptyMealSuccessAction action)
            => s_adapter.Add(state, action.Meal);

        [ReducerMethod]
        public static MealState ReduceUpdateMealSuccessAction(MealState state, UpdateMealSuccessAction action)
            => s_adapter.Update(state, action.Meal);

        [ReducerMethod]
        public static MealState ReduceDeleteMealSuccessAction(MealState state, DeleteMealSuccessAction action)
            => s_adapter.Remove(state, action.Id);
    }
}
