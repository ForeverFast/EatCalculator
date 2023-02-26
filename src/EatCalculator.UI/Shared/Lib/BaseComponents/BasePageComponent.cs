namespace EatCalculator.UI.Shared.Lib.BaseComponents
{
    public abstract class BasePageComponent : BaseFluxorComponent
    {
        #region Injects

        [Inject] protected NavigationManager _navigationManager { get; init; } = null!;

        #endregion
    }
}
