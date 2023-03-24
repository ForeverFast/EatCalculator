using EatCalculator.UI.Entities.Days.Models.Store;
using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Shared.Components;

namespace EatCalculator.UI.App.Components
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
