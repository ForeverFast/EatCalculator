using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Entities.Days.Models.Store
{
    internal sealed record DayState : EntityState<int, Day>
    {
        public int? CurrentDayId { get; init; }
    }
}
