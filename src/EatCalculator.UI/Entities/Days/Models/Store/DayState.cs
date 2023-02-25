using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Components;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Entities.Days.Models.Store
{
    internal sealed record DayState : EntityState<int, Day>
    {
        public LoadingState LoadingState { get; init; }
    }
}
