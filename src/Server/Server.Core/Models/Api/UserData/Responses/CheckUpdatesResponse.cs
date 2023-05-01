namespace Server.Core.Models.Api.UserData.Responses
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
