using DALQueryChain.Interfaces;
using Server.Core.Context.Entities.UserData;

namespace Server.Core.Context.Entities.Identity
{
    internal record User : IDbModelBase
    {
        public required int Id { get; init; }

        public required string Email { get; init; }
        public required string UserName { get; init; }

        public required string PasswordHash { get; init; }
        public string? RefreshToken { get; init; }
        public DateTime? RefreshTokenExpiryTime { get; init; }

        public UserEatData UserEatData { get; init; } = null!;
    }
}
