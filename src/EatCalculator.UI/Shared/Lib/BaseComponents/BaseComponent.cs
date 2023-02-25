namespace EatCalculator.UI.Shared.Lib.BaseComponents
{
    public abstract class BaseComponent : MudComponentBase
    {
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
    }
}
