namespace Client.Core.Shared.Api.HttpClient.Requests.Identity
{
    public record RefreshTokenRequest
    {
        public required RefreshTokenRequestData Data { get; init; }
    }

    public record RefreshTokenRequestData
    {
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
    }
}
