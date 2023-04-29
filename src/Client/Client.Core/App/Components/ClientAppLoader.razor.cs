using Client.Core.Entities.Days.Models.Store;
using Client.Core.Entities.Days.Models.Store.Actions;
using Client.Core.Entities.Products.Models.Store;
using Client.Core.Entities.Products.Models.Store.Actions;
using Client.Core.Entities.Viewer.Models.Store;
using Client.Core.Shared.Api.LocalDatabase.Context;
using Client.Core.Shared.Lib.BaseComponents;

namespace Client.Core.App.Components
{
    public partial class ClientAppLoader : BaseFluxorComponent
    {
        #region Params

        [Parameter, EditorRequired] public required RenderFragment Content { get; set; }

        #endregion

        #region Injects

        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;
        [Inject] DayStateFacade _dayStateFacade { get; init; } = null!;
        [Inject] ViewerStateFacade _viewerStateFacade { get; init; } = null!;
        [Inject] IDalQcWrapper _dalQcWrapper { get; init; } = null!;  

        #endregion

        #region UI Fields

        private LoadingState _productsLoadingState;
        private LoadingState _daysLoadingState;
        private LoadingState _state
            => LoadingStateHelper.HandleLoadingStates(
                _productsLoadingState,
                _daysLoadingState);

        private string _loadingText = "Загрузка...";

        #endregion

        #region LC Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SubscribeToAction<LoadProductsSuccessAction>(_ => _productsLoadingState = LoadingState.Content);
            SubscribeToAction<LoadDaysSuccessAction>(_ => _daysLoadingState = LoadingState.Content);

            _dalQcWrapper.DbActivated += OnDbActivated;
            _dalQcWrapper.DbDisposed += OnDbDisposed;

            TriggerLoadData();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dalQcWrapper.DbActivated -= OnDbActivated;
            _dalQcWrapper.DbDisposed -= OnDbDisposed;
        }

        #endregion

        #region External events

        private void OnDbActivated()
            => TriggerLoadData();

        private Task OnDbDisposed()
        {
            _productsLoadingState = LoadingState.NoData;
            _daysLoadingState = LoadingState.NoData;

            return Task.CompletedTask;
        }

        #endregion

        #region Private methods

        private void TriggerLoadData()
        {
            if ((!_productsLoadingState.IsNoDataState()
                && !_daysLoadingState.IsNoDataState())
                || _dalQcWrapper.State is not DalQcState.Active)
                return;

            _productsLoadingState = LoadingState.Loading;
            _productStateFacade.LoadProducts();

            _daysLoadingState = LoadingState.Loading;
            _dayStateFacade.LoadDays();
        }

        #endregion
    }
}
