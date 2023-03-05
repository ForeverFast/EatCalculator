using EatCalculator.UI.Entities.Days.Models.Store;
using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Features.Days
{
    public partial class DeleteDayButton : BaseComponent
    {
        #region Params

        [Parameter, EditorRequired] public required Day Day { get; set; }

        #endregion

        #region Injects

        [Inject] DayStateFacade _dayStateFacade { get; init; } = null!;

        #endregion

        #region Internal events

        private void OnClick()
            => _dayStateFacade.DeleteDay(Day.Id);

        #endregion
    }
}
