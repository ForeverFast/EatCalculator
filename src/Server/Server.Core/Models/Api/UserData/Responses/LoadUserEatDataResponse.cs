namespace Server.Core.Models.Api.UserData.Responses
{
    public record LoadUserEatDataResponse
    {
        public required byte[] Data { get; set; }
        public required DateTime LastUpdateDate { get; init; }
    }
}
