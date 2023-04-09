namespace Client.Core.Shared.Api.HttpClient.Responses.UserData
{
    public record CheckUpdatesResponse
    {
        public required bool AnyUpdates { get; init; }
    }
}
