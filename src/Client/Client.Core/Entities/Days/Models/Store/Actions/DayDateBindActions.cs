namespace Client.Core.Entities.Days.Models.Store.Actions
{
    internal record AttachDayToDateAction : BaseAction
    {
        public required int DayId { get; init; }
        public required DateOnly Date { get; init; }
    }
    internal record AttachDayToDateFailureAction : BaseFailureAction;
    internal record AttachDayToDateSuccessAction : BaseSuccessAction
    {
        public required DayDateBind DayDateBind { get; init; }
    }

    internal record DetachDayFromDateAction : BaseAction
    {
        public required int DayId { get; init; }
        public required int DayDateBindId { get; init; }
    }
    internal record DetachDayFromDateFailureAction : BaseFailureAction;
    internal record DetachDayFromDateSuccessAction : BaseSuccessAction
    {
        public required int DayId { get; init; }
        public required int DayDateBindId { get; init; }
    }
}
