namespace EatCalculator.UI.Shared.Components
{
    public partial class MobileBottomInfoBlock
    {
        #region Params

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public bool IsVisible { get; set; }
        [Parameter] public EventCallback<bool> IsVisibleChanged { get; set; }

        #endregion

        #region Css/Style

        protected override string ClassName
            => new CssBuilder()
            .AddClass(Class)
            .AddClass("active", IsVisible)
            .Build();

        #endregion

        #region Internal events

        private void OnSwipe(SwipeDirection direction)
        {
            Action? action = direction switch
            {
                { } when IsVisible && direction == SwipeDirection.TopToBottom => () => FireIsVisibleChange(false),
                { } when !IsVisible && direction == SwipeDirection.BottomToTop => () => FireIsVisibleChange(true),
                _ => null,
            };

            action?.Invoke();
            StateHasChanged();
        }

        #endregion

        #region Private methods

        private void FireIsVisibleChange(bool value)
        {
            IsVisible = value;
            IsVisibleChanged.InvokeAsync(IsVisible).AndForget();
        }

        #endregion
    }
}
