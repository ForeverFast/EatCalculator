using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Days.Models.Store.Actions
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
