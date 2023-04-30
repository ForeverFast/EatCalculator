using Client.Core.Entities.Days.Models.Store;
using Client.Core.Entities.Days.Models.Store.Actions;
using Client.Core.Entities.Products.Models.Store;
using Client.Core.Entities.Products.Models.Store.Actions;
using Client.Core.Shared.Api.LocalDatabase.Context;
using Client.Core.Shared.Lib.BaseComponents;
using MediatR.Courier;

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
        [Inject] ICourier _courier { get; init; } = null!;
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

            _courier.Subscribe<DbActivatedNotification>(OnDbActivated);
            _courier.Subscribe<DbDisposedNotification>(OnDbDisposed);

            //_dalQcWrapperEventPublisher.DbActivated += OnDbActivated;
            //_dalQcWrapperEventPublisher.DbDisposed += OnDbDisposed;

            TriggerLoadData();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            //_dalQcWrapperEventPublisher.DbActivated -= OnDbActivated;
            //_dalQcWrapperEventPublisher.DbDisposed -= OnDbDisposed;
            _courier.UnSubscribe<DbActivatedNotification>(OnDbActivated);
            _courier.UnSubscribe<DbDisposedNotification>(OnDbDisposed);
        }

        #endregion

        #region External events

        private void OnDbActivated(DbActivatedNotification _)
            => TriggerLoadData();

        private Task OnDbDisposed(DbDisposedNotification _)
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
