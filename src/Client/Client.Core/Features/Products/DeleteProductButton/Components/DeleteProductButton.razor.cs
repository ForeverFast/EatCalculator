using Client.Core.Entities.Products.Models.Store;

namespace Client.Core.Features.Products.DeleteProductButton
{
    public partial class DeleteProductButton : BaseComponent
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
