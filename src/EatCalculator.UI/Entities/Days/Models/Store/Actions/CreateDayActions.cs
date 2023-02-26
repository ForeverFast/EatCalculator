using EatCalculator.UI.Entities.Days.Models.Contracts;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Days.Models.Store.Actions
{
    internal record CreateDayAction : BaseAction
    {
        public required CreateDayContract Day { get; init; }
    }

    internal record CreateDayFailureAction : BaseFailureAction;

    internal record CreateDaySuccessAction : BaseSuccessAction
    {
        public required Day Day { get; init; }
    }
}
