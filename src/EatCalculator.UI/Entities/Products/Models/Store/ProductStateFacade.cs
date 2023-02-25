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
            //ProductStateSelector = _store.SubscribeSelector(ProductStateSelectors.SelectFeatureState);
            //ProductsListSelector = _store.SubscribeSelector(ProductStateSelectors.SelectProducts);
        }

        #endregion

        #region Selectors

        public override ProductState State => ProductStateSelector.Value;

        public ISelectorSubscription<ProductState> ProductStateSelector { get; }

        //public ISelectorSubscription<List<Product>> ProductsListSelector { get; }

        #endregion

        public bool IsNoDataState()
            => State.LoadingState.IsNoDataState();

        public void LoadProducts()
            => _dispatcher.Dispatch(new LoadProductsAction { });

        public void CreateProduct(CreateProductContract product)
            => _dispatcher.Dispatch(new CreateProductAction { Product = product, });

        public void DeleteProduct(int productId)
            => _dispatcher.Dispatch(new DeleteProductAction { Id = productId, });


        public void Dispose()
        {
            ProductStateSelector?.Dispose();
            //ProductsListSelector.Dispose(); 
        }
    }
}
