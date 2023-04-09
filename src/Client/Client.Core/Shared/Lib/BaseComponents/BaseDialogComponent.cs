namespace Client.Core.Shared.Lib.BaseComponents
{
    public abstract class BaseDialogComponent : BaseFluxorComponent
    {
        #region Params

        [CascadingParameter] public required MudDialogInstance MudDialog { get; set; }

        #endregion
    }
}
