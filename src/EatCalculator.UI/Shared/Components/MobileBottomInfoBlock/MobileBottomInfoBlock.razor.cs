namespace EatCalculator.UI.Shared.Components
{
    public partial class MobileBottomInfoBlock
    {
        #region Params

        [Parameter] public RenderFragment? ChildContent { get; set; }

        #endregion

        #region UI Fields

        private bool _showInfoBlock = false;

        #endregion

        #region Internal events

        private void OnSwipe(SwipeDirection direction)
        {
            _ = direction switch
            {
                { } when _showInfoBlock && direction == SwipeDirection.TopToBottom => _showInfoBlock = false,
                { } when !_showInfoBlock && direction == SwipeDirection.BottomToTop => _showInfoBlock = true,
                _ => false,
            };

            StateHasChanged();
        }

        #endregion
    }
}
