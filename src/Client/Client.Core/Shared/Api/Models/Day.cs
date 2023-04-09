using DALQueryChain.Interfaces;

namespace Client.Core.Shared.Api.Models
{
    public record Day : AdapterEntity, IDbModelBase
    {
        public required int Id { get; init; }

        public required string Title { get; init; }
        public string? Description { get; set; }

        /// <summary>
        /// Белки (100г.)
        /// </summary>
        public double ProteinPercentages { get; init; }
        /// <summary>
        /// Жиры (100г.)
        /// </summary>
        public double FatPercentages { get; init; }
        /// <summary>
        /// Углеводы (100г.)
        /// </summary>
        public double CarbohydratePercentages { get; init; }

        public int ProteinMealCount { get; init; }
        public int FatMealCount { get; init; }
        public int CarbohydrateMealCount { get; init; }

        public int Order { get; init; }


        public List<DayDateBind> DayDateBinds { get; init; } = new();
        public List<Meal> Meals { get; init; } = new();
    }
}
