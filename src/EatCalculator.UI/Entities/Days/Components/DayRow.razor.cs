using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Entities.Days.Components
{
    public partial class DayRow : BaseComponent
    {
        #region Params

        [Parameter, EditorRequired] public required Day Day { get; set; }

        [Parameter] public RenderFragment? OptionalContent { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        #endregion
    }
}
