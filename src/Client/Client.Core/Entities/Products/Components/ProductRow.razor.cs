namespace Client.Core.Entities.Products
{
    public partial class ProductRow : BaseComponent
    {
        #region Params

        [Parameter, EditorRequired] public required Product Product { get; set; }

        [Parameter] public RenderFragment? OptionalContent { get; set; }
        [Parameter] public RenderFragment? BottomContent { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        #endregion
    }
}
