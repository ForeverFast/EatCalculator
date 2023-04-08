﻿using DALQueryChain.Interfaces;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Shared.Api.Models
{
    public record Portion : AdapterEntity, IDbModelBase
    {
        public required int Id { get; init; }
        public required int MealId { get; init; }
        public required int ProductId { get; init; }

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

        public Product Product { get; init; } = null!;
    }
}
