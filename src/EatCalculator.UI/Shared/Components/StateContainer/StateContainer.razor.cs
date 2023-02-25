namespace EatCalculator.UI.Shared.Components
{
    public partial class StateContainer
    {
        #region Params

        [Parameter] public LoadingState State { get; set; } = LoadingState.NotTriggered;

        [Parameter] public RenderFragment? NotTriggered { get; set; }
        [Parameter] public RenderFragment? Loading { get; set; }
        [Parameter] public RenderFragment? NoData { get; set; }
        [Parameter] public RenderFragment? Content { get; set; }
        [Parameter] public RenderFragment? Error { get; set; }

        #endregion

        private RenderFragment? GetComponentStateFragment()
            => State switch
            {
                LoadingState.NotTriggered => NotTriggered,
                LoadingState.Loading => Loading,
                LoadingState.NoData => NoData,
                LoadingState.Content => Content,
                LoadingState.Error => Error,
                _ => Loading
            };
    }
}
