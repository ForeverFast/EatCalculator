namespace Client.Core.Entities.Viewer.Models.Store.Actions
{
    internal record ViewerEatDataFailureAction : BaseFailureAction;
    internal record ViewerEatDataSuccessAction : BaseSuccessAction
    {
        public required DateTime LastDbUpdateDate { get; init; }
    }
}
