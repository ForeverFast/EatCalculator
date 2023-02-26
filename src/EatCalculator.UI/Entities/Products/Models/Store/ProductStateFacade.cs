using EatCalculator.UI.Entities.Products.Models.Contracts;
using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Components;
using EatCalculator.UI.Shared.Lib.Fluxor;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Entities.Products.Models.Store
{
    internal sealed class ProductStateFacade : StateFacade<ProductState>, IDisposable
    {
        #region Ctors

        public ProductStateFacade(IStore store,
                                  IDispatcher dispatcher) : base(store, dispatcher)
        {
            ListSelector = _store.SubscribeSelector(ProductStateSelectors.SelectProducts);
            LoadingStateSelector = _store.SubscribeSelector(ProductStateSelectors.SelectProductsLoadingState);
        }

        #endregion

        #region Selectors

        protected override ISelector<ProductState> StateSelectorPointer
            => ProductStateSelectors.SelectFeatureState;

        public ISelectorSubscription<List<Product>> ListSelector { get; }
        public ISelectorSubscription<LoadingState> LoadingStateSelector { get; }

        #endregion

        public bool IsNoDataState()
            => StateSelector.Value.LoadingState.IsNoDataState();

        public void LoadProducts()
            => _dispatcher.Dispatch(new LoadProductsAction { });

        public void CreateProduct(CreateProductContract product)
            => _dispatcher.Dispatch(new CreateProductAction { Product = product, });

        public void UpdateProduct(int id, UpdateProductContract product)
            => _dispatcher.Dispatch(new UpdateProductAction
            {
                Id = id,
                Product = product,
            });

        public void DeleteProduct(int productId)
            => _dispatcher.Dispatch(new DeleteProductAction { Id = productId, });



        public void Dispose()
        {
            StateSelector?.Dispose();
            ListSelector.Dispose();
        }
    }
}
