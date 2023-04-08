﻿using DALQueryChain.Interfaces;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Shared.Api.Models
{
    public record Meal : AdapterEntity, IDbModelBase
    {
        public required int Id { get; init; }
        public required int DayId { get; init; }

        public required string Title { get; init; }

        public int Order { get; init; }

        public List<Portion> Portions { get; init; } = new();
    }
}
