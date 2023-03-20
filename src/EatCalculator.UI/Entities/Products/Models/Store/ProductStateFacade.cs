using EatCalculator.UI.Entities.Products.Models.Contracts;
using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Entities.Products.Models.Store
{
    internal sealed class ProductStateFacade : StateFacade<ProductState>
    {
        #region Ctors

        public ProductStateFacade(IStore store,
                                  IDispatcher dispatcher) : base(store, dispatcher)
        {
            Products = _store.SubscribeSelector(ProductStateSelectors.SelectProducts);
        }

        #endregion

        #region Selectors

        protected override ISelector<ProductState> SelectState
            => ProductStateSelectors.SelectFeatureState;

        public ISelectorSubscription<List<Product>> Products { get; }

        #endregion

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

        public Product? GetProductById(int productId) 
            => State.Value.Entities.FirstOrDefault(x => x.Key == productId).Value;    


        public override void Dispose()
        {
            base.Dispose();

            Products.Dispose();
        }
    }
}
