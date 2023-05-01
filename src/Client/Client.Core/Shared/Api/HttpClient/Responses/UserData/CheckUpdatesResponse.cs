namespace Client.Core.Shared.Api.HttpClient.Responses.UserData
{
    public record CheckUpdatesResponse
    {
        public required ServerDataState ServerDataState { get; init; }
    }

    public enum ServerDataState
    {
        NeedUpdate,
        NoUpdates,
        NotFound,
    }
}
