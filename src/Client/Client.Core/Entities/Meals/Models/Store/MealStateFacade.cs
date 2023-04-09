using Client.Core.Entities.Meals.Models.Contracts;
using Client.Core.Entities.Meals.Models.Store.Actions;

namespace Client.Core.Entities.Meals.Models.Store
{
    internal sealed class MealStateFacade : StateFacade<MealState>
    {
        #region Ctors

        public MealStateFacade(IStore store,
                               IDispatcher dispatcher) : base(store, dispatcher)
        {
            CurrentMeal = _store.SubscribeSelector(MealStateSelectors.SelectCurrentMeal);
            Meals = _store.SubscribeSelector(MealStateSelectors.SelectMeals);
        }

        #endregion

        #region Selectors

        protected override ISelector<MealState> SelectState
            => MealStateSelectors.SelectFeatureState;

        public ISelectorSubscription<Meal?> CurrentMeal { get; }
        public ISelectorSubscription<List<Meal>> Meals { get; }

        #endregion

        public void LoadMeals(int dayId)
            => _dispatcher.Dispatch(new LoadMealsAction
            {
                DayId = dayId,
            });

        public void CreateMeal(CreateMealContract meal)
            => _dispatcher.Dispatch(new CreateMealAction
            {
                Meal = meal
            });

        public void CreateEmptyMeal(int dayId)
           => _dispatcher.Dispatch(new CreateEmptyMealAction
           {
               DayId = dayId,
           });

        public void UpdateMeal(int id, UpdateMealContract meal)
            => _dispatcher.Dispatch(new UpdateMealAction
            {
                Id = id,
                Meal = meal,
            });

        public void DeleteMeal(int id)
            => _dispatcher.Dispatch(new DeleteMealAction
            {
                Id = id,
            });
    }
}
