using DALQueryChain.Interfaces;

namespace Client.Core.Shared.Api.Models
{
    public record Day : AdapterEntity, IDbModelBase
    {
        public required int Id { get; init; }

        public required string Title { get; init; }
        public string? Description { get; init; }

        public required double Kilocalories { get; init; }
        /// <summary>
        /// Белки (100г.)
        /// </summary>
        public required double ProteinPercentages { get; init; }
        /// <summary>
        /// Жиры (100г.)
        /// </summary>
        public required double FatPercentages { get; init; }
        /// <summary>
        /// Углеводы (100г.)
        /// </summary>
        public required double CarbohydratePercentages { get; init; }

        public required int ProteinMealCount { get; init; }
        public required int FatMealCount { get; init; }
        public required int CarbohydrateMealCount { get; init; }

        public int Order { get; init; }


        public List<DayDateBind> DayDateBinds { get; init; } = new();
        public List<Meal> Meals { get; init; } = new();
    }
}
