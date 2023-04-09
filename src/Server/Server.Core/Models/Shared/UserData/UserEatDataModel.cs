namespace Server.Core.Models.Shared.UserData
{
    public record UserEatDataModel
    {
        public required int UserId { get; init; }
        public DateTime? LastUpdateDate { get; init; }
    }
}
