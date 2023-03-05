using EatCalculator.UI.Entities.Days.Models.Store;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Configs;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Pages.Days
{
    [Route($"{Routes.Days.BasePath}/{{DayId:int}}/{Routes.Days.Update}")]
    public partial class UpdateDayPage : BasePageComponent
    {
        #region Params

        [Parameter] public int DayId { get; set; }

        #endregion

        #region Injects

        [Inject] DayStateFacade _dayStateFacade { get; init; } = null!;

        #endregion

        #region UI Fields

        #endregion

        #region Selectors

        private ISelectorSubscription<Day> _currentDaySelector
            => _dayStateFacade.CurrentDaySelector;

        #endregion

        #region State methods

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _dayStateFacade.SelectDay(DayId);
        }

        #endregion
    }
}
