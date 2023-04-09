namespace Server.Core.Models.Shared.Identity
{
    public record UserModel
    {
        public required int Id { get; init; }
        public required string Email { get; init; }
        public required string UserName { get; init; }
    }
}
