namespace UI
{
    public abstract class BaseComponent : MudComponentBase
    {
        #region Css/Style

        protected string GetCssName(string componentName)
            => componentName.PascalToKebabCase();

        protected virtual string ClassName
            => new CssBuilder()
            .AddClass(Class)
            .Build();

        protected virtual string StyleName
            => new StyleBuilder()
            .AddStyle(Style)
            .Build();

        #endregion
    }
}
