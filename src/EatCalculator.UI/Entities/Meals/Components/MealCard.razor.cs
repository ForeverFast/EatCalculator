using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Entities.Meals.Components
{
    public partial class MealCard
    {
        #region Params

        [Parameter, EditorRequired] public required Meal Meal { get; set; }

        [Parameter] public RenderFragment? OptionalContent { get; set; }
        [Parameter] public RenderFragment? BottomContent { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        #endregion
    }
}
