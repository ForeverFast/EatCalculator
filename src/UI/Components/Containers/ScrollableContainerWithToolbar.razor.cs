namespace UI
{
    public partial class ScrollableContainerWithToolbar
    {
        #region Params

        [Parameter, EditorRequired] public required RenderFragment ToolbarContent { get; set; }
        [Parameter] public RenderFragment? AdditionalToolbarContent { get; set; }
        [Parameter, EditorRequired] public required RenderFragment Content { get; set; }
        [Parameter] public RenderFragment? BottomContent { get; set; }

        #endregion
    }
}
