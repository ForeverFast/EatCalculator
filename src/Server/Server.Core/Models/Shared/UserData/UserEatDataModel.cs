namespace Server.Core.Models.Shared.UserData
{
    public record UserEatDataModel
    {
        public required int Id { get; init; }
        public DateTime? LastUpdateDate { get; init; }
    }
}
