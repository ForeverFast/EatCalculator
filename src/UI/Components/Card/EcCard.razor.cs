namespace UI
{
    public partial class EcCard : BaseComponent
    {
        #region Params

        [Parameter, EditorRequired] public required string Title { get; set; }

        [Parameter] public RenderFragment? OptionalContent { get; set; }
        [Parameter] public RenderFragment? BottomContent { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        #endregion
    }
}
