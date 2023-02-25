using Fluxor.Blazor.Web.Components;

namespace EatCalculator.UI.Shared.Lib.BaseComponents
{
    public abstract class BaseFluxorComponent : FluxorComponent
    {
        #region Injects

        [Inject] protected IDispatcher _dispatcher { get; init; } = null!;

        #endregion

        protected void OnStateHasChangedAction(object action)
           => InvokeAsync(() => OnStateHasChangedActionExecute(action));

        protected virtual void OnStateHasChangedActionExecute(object _)
            => StateHasChanged();

        protected Task BeforeStateHasChanged(Action action)
            => InvokeAsync(() =>
            {
                action();
                StateHasChanged();
            });

        protected Task BeforeStateHasChanged(Func<Task> action)
            => InvokeAsync(async () =>
            {
                await action();
                StateHasChanged();
            });
    }
}
