namespace EatCalculator.UI.Shared.Components
{
    public partial class ScrollableContainerWithToolbar
    {
        #region Params

        [Parameter, EditorRequired] public required RenderFragment ToolbarContent { get; set; } 
        [Parameter] public RenderFragment? AdditionalToolbarContent { get; set; } 
        [Parameter, EditorRequired] public required RenderFragment Content { get; set; }
        [Parameter] public RenderFragment? BottomContent { get; set; }

        [Parameter] public bool DisableScroll { get; set; }

        #endregion

        #region Css/Style

        private string ScrollableDivCssName
            => new CssBuilder("")
            .AddClass("", !DisableScroll)
            .AddClass("overflow-y-hidden", DisableScroll)
            .Build();

        #endregion
    }
}
