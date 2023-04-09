namespace Client.Core.Entities.Days.Models.Store.Actions
{
    internal record SelectDayAction : BaseAction
    {
        public required int DayId { get; init; }
    }
}
