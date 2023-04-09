using Client.Core.Entities.Meals.Models.Store;

namespace Client.Core.Features.Meals.DeleteMealButton
{
    public partial class DeleteMealButton : BaseComponent
    {
        #region Params

        [Parameter, EditorRequired] public required Meal Meal { get; set; }

        #endregion

        #region Injects

        [Inject] MealStateFacade _mealStateFacade { get; init; } = null!;

        #endregion

        #region Internal events

        private void OnClick()
            => _mealStateFacade.DeleteMeal(Meal.Id);

        #endregion
    }
}
