namespace Client.Core.Entities.Days.Models.Store
{
    internal sealed record DayState : EntityState<int, Day>
    {
        public int? CurrentDayId { get; init; }
    }
}
