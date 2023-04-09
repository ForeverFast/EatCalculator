using Client.Core.Entities.Days.Models.Store;
using Client.Core.Features.Days.CreateDayDialog;
using Client.Core.Shared.Lib;
using Client.Core.Shared.Lib.BaseComponents;

namespace Client.Core.Pages
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
