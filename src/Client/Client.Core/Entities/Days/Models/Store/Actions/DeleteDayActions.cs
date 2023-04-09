namespace Client.Core.Entities.Days.Models.Store.Actions
{
    internal record DeleteDayAction : BaseAction
    {
        public required int Id { get; init; }
    }
    internal record DeleteDayFailureAction : BaseFailureAction;
    internal record DeleteDaySuccessAction : BaseSuccessAction
    {
        public required int Id { get; init; }
    }
}
