using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Features.Products
{
    public partial class DeleteProduct : BaseComponent
    {
        #region Params

        [Parameter, EditorRequired] public required Product Product { get; set; }

        #endregion

        #region Injects

        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;

        #endregion

        #region Internal events

        private void OnClick()
            => _productStateFacade.DeleteProduct(Product.Id);

        #endregion
    }
}
