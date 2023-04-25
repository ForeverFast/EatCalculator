namespace Client.Core.Entities.Viewer.Models.Store.Actions
{
    internal record SynchronizeEatDataAction : BaseAction;
    internal record SynchronizeEatDataFailureAction : BaseFailureAction;
    internal record SynchronizeEatDataSuccessAction : BaseSuccessAction
    {
        public required DateTime LastDbUpdateDate { get; init; }
    }
}
