namespace Server.Core.Models.Api.Identity.Responses
{
    public record RefreshTokenResponse
    {
        public required string AccessToken { get; init; }
    }
}
