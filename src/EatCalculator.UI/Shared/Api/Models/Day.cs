using DALQueryChain.Interfaces;

namespace EatCalculator.UI.Shared.Api.Models
{
    public record Day : IDbModelBase
    {
        public required int Id { get; init; }

        public required string Title { get; init; }
        public string? Description { get; set; }
        public int MealCount { get; init; }

        /// <summary>
        /// Белки (100г.)
        /// </summary>
        public double ProteinTotal { get; init; }
        public double ProteinPercentages { get; init; }
        public int ProteinPortions { get; init; }

        /// <summary>
        /// Жиры (100г.)
        /// </summary>
        public double FatTotal { get; init; }
        public double FatPercentages { get; init; }
        public int FatPortions { get; init; }

        /// <summary>
        /// Углеводы (100г.)
        /// </summary>
        public double CarbohydrateTotal { get; init; }
        public double CarbohydratePercentages { get; init; }
        public int CarbohydratePortions { get; init; }

        public int Order { get; init; }



        public List<Meal> Meals { get; init; } = new List<Meal>();
    }
}
