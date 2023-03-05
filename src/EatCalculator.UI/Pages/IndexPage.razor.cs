using EatCalculator.UI.Entities.Days.Models.Store;
using EatCalculator.UI.Shared.Api.Models;
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
            => _dayStateFacade.ListSelector;

        #endregion
    }
}
