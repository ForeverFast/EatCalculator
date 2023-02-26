namespace EatCalculator.UI.Entities.Days.Models.Contracts
{
    internal sealed record CreateDayContract
    {
        public required string Title { get; init; }
    }
}
