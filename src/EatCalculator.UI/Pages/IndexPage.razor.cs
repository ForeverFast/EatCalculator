using EatCalculator.UI.Entities.Days.Models.Store;
using EatCalculator.UI.Features.Days.CreateDayDialog;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Pages
{
    [Route("/")]
    public partial class IndexPage : BasePageComponent
    {
        #region Injects

        [Inject] DayStateFacade _dayStateFacade { get; init; } = null!;

        #endregion

        #region Selectors

        private ISelectorSubscription<List<Day>> _dayListSelector
            => _dayStateFacade.Days;

        #endregion

        #region Internal events

        private void OnCreateDayButtonClick()
            => _dialogService.OpenCreateDayDialog();

        private void OnDayRowClick(Day day)
            => _navigationManager.NavigateToDayPage(day.Id);

        #endregion
    }
}
