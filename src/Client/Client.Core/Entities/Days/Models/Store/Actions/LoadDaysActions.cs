namespace Client.Core.Entities.Days.Models.Store.Actions
{
    internal record LoadDaysAction : BaseAction;
    internal record LoadDaysFailureAction : BaseFailureAction;
    internal record LoadDaysSuccessAction : BaseSuccessAction
    {
        public required IEnumerable<Day> Days { get; init; }
    }
}
