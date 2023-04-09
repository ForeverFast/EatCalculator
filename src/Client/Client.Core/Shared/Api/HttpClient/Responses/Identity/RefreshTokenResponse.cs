namespace Client.Core.Shared.Api.HttpClient.Responses.Identity
{
    public record RefreshTokenResponse
    {
        public required string AccessToken { get; init; }
    }
}
