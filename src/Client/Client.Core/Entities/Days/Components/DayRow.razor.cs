namespace Client.Core.Entities.Days
{
    public partial class DayRow : BaseComponent
    {
        #region Params

        [Parameter, EditorRequired] public required Day Day { get; set; }

        [Parameter] public RenderFragment? OptionalContent { get; set; }
        [Parameter] public RenderFragment? BottomContent { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        #endregion
    }
}
