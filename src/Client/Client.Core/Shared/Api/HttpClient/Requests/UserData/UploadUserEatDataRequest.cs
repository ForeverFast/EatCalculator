namespace Client.Core.Shared.Api.HttpClient.Requests.UserData
{
    public record UploadUserEatDataRequest
    {
        public required byte[] DbFile { get; init; }
    }
}
