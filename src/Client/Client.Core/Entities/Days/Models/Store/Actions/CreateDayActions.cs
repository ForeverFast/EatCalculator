using Client.Core.Entities.Days.Models.Contracts;

namespace Client.Core.Entities.Days.Models.Store.Actions
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
