using Client.Core.Entities.Days.Models.Contracts;

namespace Client.Core.Entities.Days.Models.Store.Actions
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
