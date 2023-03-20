using DALQueryChain.Interfaces;

namespace EatCalculator.UI.Shared.Api.Models
{
    public record Day : IDbModelBase
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


        public int Order { get; init; }


        public List<Meal> Meals { get; init; } = new List<Meal>();
    }
}
