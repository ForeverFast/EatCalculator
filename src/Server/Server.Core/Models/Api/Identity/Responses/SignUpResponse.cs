namespace Server.Core.Models.Api.Identity.Responses
{
    public record SignUpResponse
    {
        public required long UserId { get; init; }
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
    }
}
