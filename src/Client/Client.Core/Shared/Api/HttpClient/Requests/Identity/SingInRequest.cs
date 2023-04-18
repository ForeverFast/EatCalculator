namespace Client.Core.Shared.Api.HttpClient.Requests.Identity
{
    public record SignInRequest
    {
        public required SignInRequestData Body { get; init; }
    }

    public record SignInRequestData
    {
        public required string Login { get; init; }
        public required string Password { get; init; }
    }
}
