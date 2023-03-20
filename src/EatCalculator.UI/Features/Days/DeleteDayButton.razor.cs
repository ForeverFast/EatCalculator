using EatCalculator.UI.Entities.Days.Models.Store;
using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Features.Days
{
    public partial class DeleteDayButton : BaseComponent
    {
        #region Params

        [Parameter, EditorRequired] public required Day Day { get; set; }

        [Parameter] public Variant Variant { get; set; }
        [Parameter] public Size Size { get; set; } = Size.Small;

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }  

        #endregion

        #region Injects

        [Inject] DayStateFacade _dayStateFacade { get; init; } = null!;

        #endregion

        #region Internal events

        private async Task OnButtonClick(MouseEventArgs args)
        {
            await OnClick.InvokeAsync(args);

            _dayStateFacade.DeleteDay(Day.Id);
        }

        #endregion
    }
}
