using DALQueryChain.Interfaces;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Shared.Api.Models
{
    public record DayDateBind : AdapterEntity, IDbModelBase
    {
        public required int Id { get; init; }
        public required int DayId { get; init; }
        public required DateOnly Date { get; init; }

        public Day Day { get; init; } = null!;
    }
}
