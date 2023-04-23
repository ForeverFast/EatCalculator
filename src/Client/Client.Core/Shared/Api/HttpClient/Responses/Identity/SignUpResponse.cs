namespace Client.Core.Shared.Api.HttpClient.Responses.Identity
{
    public record SignUpResponse
    {
        public required long UserId { get; init; }
        public required string AccessToken { get; init; }
    }
}
