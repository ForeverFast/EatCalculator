using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Entities.Products.Components
{
    public partial class ProductRow : BaseComponent
    {
        #region Params

        [Parameter, EditorRequired] public required Product Product { get; set; }

        [Parameter] public RenderFragment? OptionalContent { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        #endregion
    }
}
