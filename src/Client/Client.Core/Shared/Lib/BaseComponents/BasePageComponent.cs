namespace Client.Core.Shared.Lib.BaseComponents
{
    public abstract class BasePageComponent : BaseFluxorComponent
    {
        #region Injects

        [Inject] protected NavigationManager _navigationManager { get; init; } = null!;
        [Inject] protected IDialogService _dialogService { get; init; } = null!;

        #endregion
    }
}
