using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Components;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;
using Fluxor.Blazor.Web.Components;

namespace EatCalculator.UI.Pages
{
    [Route("/")]
    public partial class IndexPage : FluxorComponent
    {
        #region Injects

        [Inject] IStore _store { get; init; } = null!; 
        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            ProductsListSelector = _store.SubscribeSelector(ProductStateSelectors.SelectProducts);
            ProductsLoadingStateSelector = _store.SubscribeSelector(ProductStateSelectors.SelectProductsLoadingState);
            
            base.OnInitializedAsync();

            //if (_productStateFacade.IsNoDataState())
                
        }

        #endregion

        private ISelectorSubscription<List<Product>> ProductsListSelector { get; set; } = default!;
        private ISelectorSubscription<LoadingState> ProductsLoadingStateSelector { get; set; } = default!;

        private void OnTestButtonClick()
        {
            _productStateFacade.LoadProducts();
        }
    }
}
