namespace Client.Core.Shared.Api.HttpClient.Requests.Identity
{
    public record SignUpRequest
    {
        public required SignUpRequestData Data { get; init; }
    }

    public record SignUpRequestData
    {
        public required string Email { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }
    }
}
