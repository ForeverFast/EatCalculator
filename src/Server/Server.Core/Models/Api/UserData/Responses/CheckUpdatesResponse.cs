namespace Server.Core.Models.Api.UserData.Responses
{
    public record CheckUpdatesResponse
    {
        public required bool AnyUpdates { get; init; }  
    }
}
