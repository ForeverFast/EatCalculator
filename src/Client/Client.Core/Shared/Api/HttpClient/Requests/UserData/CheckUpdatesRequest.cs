namespace Client.Core.Shared.Api.HttpClient.Requests.UserData
{
    public record CheckUpdatesRequest
    {
        public DateTime? LastUpdateDate { get; init; }
    }
}
