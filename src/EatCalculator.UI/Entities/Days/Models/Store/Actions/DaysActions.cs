using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Days.Models.Store.Actions
{
    internal record SelectDayAction : BaseAction
    {
        public required int DayId { get; init; }
    }
}
