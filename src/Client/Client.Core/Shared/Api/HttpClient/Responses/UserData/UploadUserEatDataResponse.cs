namespace Client.Core.Shared.Api.HttpClient.Responses.UserData
{
    public record UploadUserEatDataResponse
    {
        public required DateTime LastUpdateDate { get; init; }
    }
}
