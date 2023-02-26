using EatCalculator.UI.Entities.Days.Models.Contracts;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Days.Models.Store.Actions
{
    internal record UpdateDayAction : BaseAction
    {
        public required int Id { get; init; }
        public required UpdateDayContract Day { get; init; }
    }
    internal record UpdateDayFailureAction : BaseFailureAction;
    internal record UpdateDaySuccessAction : BaseSuccessAction
    {
        public required Day Day { get; init; }
    }
}
