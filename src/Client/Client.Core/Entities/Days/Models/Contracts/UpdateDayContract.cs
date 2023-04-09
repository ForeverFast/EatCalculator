namespace Client.Core.Entities.Days.Models.Contracts
{
    internal sealed record UpdateDayContract
    {
        public required string Title { get; init; }
        public string? Description { get; set; }

        public required double ProteinPercentages { get; init; }
        public required double FatPercentages { get; init; }
        public required double CarbohydratePercentages { get; init; }

        public required int ProteinMealCount { get; init; }
        public required int FatMealCount { get; init; }
        public required int CarbohydrateMealCount { get; init; }
    }
}
