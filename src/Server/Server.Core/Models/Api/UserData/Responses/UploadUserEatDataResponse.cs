namespace Server.Core.Models.Api.UserData.Responses
{
    public record UploadUserEatDataResponse
    {
        public required DateTime LastUpdateDate { get; init; }
    }
}
