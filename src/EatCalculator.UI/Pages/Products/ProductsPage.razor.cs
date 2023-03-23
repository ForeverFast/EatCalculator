﻿using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Features.Products.CreateProductDialog.Lib;
using EatCalculator.UI.Features.Products.UpdateProductDialog.Lib;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Configs;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Pages.Products
{
    [Route($"{Routes.Products.BasePath}")]
    public partial class ProductsPage : BasePageComponent
    {
        #region Injects

        [Inject] IDialogService _dialogService { get; init; } = null!;

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
