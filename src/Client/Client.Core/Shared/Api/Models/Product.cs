using DALQueryChain.Interfaces;

namespace Client.Core.Shared.Api.Models
{
    public record Product : IDbModelBase
    {
        public required int Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; set; }

        public required double Grams { get; init; }

        /// <summary>
        /// Белки на Grams
        /// </summary>
        public required double Protein { get; init; }
        /// <summary>
        /// Жиры на Grams
        /// </summary>
        public required double Fat { get; set; }
        /// <summary>
        /// Углеводы на Grams
        /// </summary>
        public required double Carbohydrate { get; init; }

        public int Order { get; init; }
    }
}
