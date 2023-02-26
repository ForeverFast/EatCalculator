using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Days.Models.Store.Actions
{
    internal record LoadDaysAction : BaseAction;
    internal record LoadDaysFailureAction : BaseFailureAction;
    internal record LoadDaysSuccessAction : BaseSuccessAction
    {
        public required IEnumerable<Day> Days { get; init; }
    }
}
