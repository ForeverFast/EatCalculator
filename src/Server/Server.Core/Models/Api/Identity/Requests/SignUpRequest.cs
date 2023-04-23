using Server.Core.Base.Requests;

namespace Server.Core.Models.Api.Identity.Requests
{
    public record SignUpRequest : BaseRequest
    {
        [FromBody] public required SignUpRequestData Data { get; init; }
    }

    public record SignUpRequestData
    {
        public required string UserName { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
