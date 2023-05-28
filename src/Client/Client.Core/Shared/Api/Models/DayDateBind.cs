using DALQueryChain.Interfaces;

namespace Client.Core.Shared.Api.Models
{
    public record DayDateBind : IDbModelBase
    {
        public required int Id { get; init; }
        public required int DayId { get; init; }
        public required DateOnly Date { get; init; }

        public Day Day { get; init; } = null!;
    }
}
