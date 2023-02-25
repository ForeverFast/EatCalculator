using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;
using Fluxor.Blazor.Web.Components;

namespace EatCalculator.UI.Pages
{
    [Route("/")]
    public partial class IndexPage : FluxorComponent
    {
        #region Injects

        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;

        #endregion

        #region Selectors

        public ISelectorSubscription<List<Product>> _productsListSelector => _productStateFacade.ProductsListSelector;

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (_productStateFacade.IsNoDataState())
                _productStateFacade.LoadProducts();
        }

        #endregion

        private void OnTestButtonClick()
        {

        }
    }
}
