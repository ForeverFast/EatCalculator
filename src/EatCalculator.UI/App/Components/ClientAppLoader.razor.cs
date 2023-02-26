using EatCalculator.UI.App.Models.Store;
using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Shared.Components;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.App.Components
{
    public partial class ClientAppLoader : BaseFluxorComponent
    {
        #region Params

        [Parameter, EditorRequired] public required RenderFragment Content { get; set; }

        #endregion

        #region Injects

        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;

        #endregion

        #region Selectors

        private ISelectorSubscription<LoadingState> _productsLoadingStateSelector
            => _productStateFacade.LoadingStateSelector;

        #endregion

        #region UI Fields

        //private LoadingState _serverUpdatesCheckLoadingState = LoadingState.NotTriggered;
        private LoadingState _progressState
            => GetLoadingState();

        private string _progressText = "Загружаем...";

        

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _progressText = "Проверяем обновления на сервере...";
            //_serverUpdatesCheckLoadingState = LoadingState.Loading;
            _productStateFacade.LoadProducts();
        }

        #endregion

        #region Private methods

        private LoadingState GetLoadingState()
            => LoadingStateHelper.HandleLoadingStates(_productsLoadingStateSelector.Value);

        #endregion
    }
}
