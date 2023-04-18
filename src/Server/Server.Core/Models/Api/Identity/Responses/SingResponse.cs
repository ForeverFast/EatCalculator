namespace Server.Core.Models.Api.Identity.Responses
{     
    public record SignInResponse
    {
        public required long UserId { get; init; }
        public required string AccessToken { get; init; }
    }
}
