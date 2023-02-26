using Fluxor.Blazor.Web.Components;

namespace EatCalculator.UI.Shared.Lib.BaseComponents
{
    public abstract class BaseFluxorComponent : FluxorComponent
    {
        #region Params

        [Parameter] public string Class { get; set; } = string.Empty;   
        [Parameter] public string Style { get; set; } = string.Empty;
        [Parameter] public object? Tag { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> UserAttributes { get; set; }
            = new Dictionary<string, object>();

        #endregion

        #region Injects

        [Inject] protected IStore _store { get; init; } = null!;
        [Inject] protected IDispatcher _dispatcher { get; init; } = null!;

        #endregion

        #region Css/Style

        protected CssBuilder GetCssBuilder(string componentName)
            => new CssBuilder(componentName.PascalToKebabCase());

        protected virtual string ClassName(string componentName)
            => GetCssBuilder(componentName)
            .AddClass(Class)
            .Build();

        protected virtual string StyleName
            => new StyleBuilder()
            .AddStyle(Style)
            .Build();

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
