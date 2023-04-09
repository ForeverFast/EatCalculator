using Client.Core.Entities.Products.Models.Store;
using Client.Core.Features.Products.CreateProductDialog;
using Client.Core.Features.Products.UpdateProductDialog;
using Client.Core.Shared.Configs;
using Client.Core.Shared.Lib.BaseComponents;

namespace Client.Core.Pages.Products
{
    [Route($"{Routes.Products.BasePath}")]
    public partial class ProductsPage : BasePageComponent
    {
        #region Injects

        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;

        #endregion

        #region Selectors

        private ISelectorSubscription<List<Product>> _productsListSelector
            => _productStateFacade.Products;

        #endregion

        #region Internal events

        private void OnCreateProductButtonClick()
            => _dialogService.OpenCreateProductDialog();

        private void OnUpdateProductButtonClick(Product product)
            => _dialogService.OpenUpdateProductDialog(product);

        #endregion
    }
}
