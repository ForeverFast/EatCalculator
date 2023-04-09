using Client.Core.Entities.Days.Models.Store;
using Client.Core.Entities.Products.Models.Store;
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

        #endregion

        #region UI Fields

        //private LoadingState _serverUpdatesCheckLoadingState = LoadingState.NotTriggered;
        private LoadingState _progressState = LoadingState.Content;

        private string _progressText = "Загружаем...";



        #endregion

        #region LC Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _progressText = "Проверяем обновления на сервере...";
            //_serverUpdatesCheckLoadingState = LoadingState.Loading;
            _productStateFacade.LoadProducts();
            _dayStateFacade.LoadDays();
        }

        #endregion

        #region Private methods

        #endregion
    }
}
