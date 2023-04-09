namespace Client.Core.Shared.Api.HttpClient.Responses.UserData
{
    public record LoadUserEatDataResponse
    {
        public required byte[] Data { get; set; }
        public required DateTime LastUpdateDate { get; init; }
    }
}
