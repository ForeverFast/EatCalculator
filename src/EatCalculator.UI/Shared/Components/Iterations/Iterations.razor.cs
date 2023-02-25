namespace EatCalculator.UI.Shared.Components
{
    public partial class Iterations<T> : ComponentBase
    {
        #region Params

        [Parameter] public IEnumerable<T>? Items { get; set; }
        [Parameter] public RenderFragment<T>? ChildContent { get; set; }

        #endregion

        #region UI Fields

        private bool _canIterateItems
            => Items?.Count() > 0
            && ChildContent is not null;

        #endregion
    }
}
