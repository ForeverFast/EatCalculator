using DALQueryChain.Interfaces;
using Server.Core.Context.Entities.Identity;

namespace Server.Core.Context.Entities.UserData
{
    internal record UserEatData : IDbModelBase
    {
        public required int Id { get; init; }
        public string? FilePath { get; init; }  
        public DateTime? LastUpdateDate { get; init; }

        public User User { get; init; } = null!;

        public bool HasData
            => !string.IsNullOrEmpty(FilePath) && LastUpdateDate.HasValue;
    }
}
