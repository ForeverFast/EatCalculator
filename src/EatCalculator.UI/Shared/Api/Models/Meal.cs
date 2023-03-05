using DALQueryChain.Interfaces;

namespace EatCalculator.UI.Shared.Api.Models
{
    public class Meal : IDbModelBase
    {
        public required int Id { get; init; }
        public required int DayId { get; init; }

        public string? Name { get; init; }

        public int Order { get; init; }

        public List<Portion> Portions { get; init; } = new List<Portion>();
    }
}
